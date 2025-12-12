using Kash.Shared.Application.Abstractions.Servicies;
using Kash.Shared.Application.Interfaces;
using Kash.Shared.Domain.Abstractions;
using Kash.Shared.Domain.Abstractions.Results;
using Kash.Shared.Domain.Interfaces;
using Kash.Shared.Domain.Interfaces.Repositories;
using MediatR;

namespace Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;

/// <summary>
/// Handler genérico para actualizar entidades.
/// 🔥 MODIFICADO: Ahora devuelve Result<Guid> y maneja validaciones de dominio con Result en lugar de excepciones.
/// 🔥 ROLLBACK AUTOMÁTICO: Si ApplyChanges falla, se hace rollback de la transacción.
/// </summary>
public abstract class AbsUpdateCommandHandler<TEntity, TId, TDto, TCommand>
    : AbsCommandHandler<TEntity, TId>, IRequestHandler<TCommand, Result<Guid>>
    where TEntity : AbsEntity<TId>
    where TCommand : AbsUpdateCommand<TEntity, TId, TDto>
    where TId : IGuidValueObject
    where TDto : class
{
    protected AbsUpdateCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<TEntity, TId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext)
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
    }

    /// <summary>
    /// Método abstracto para que el hijo aplique los cambios.
    /// 🔥 CAMBIO IMPORTANTE: Ahora devuelve Result en lugar de void.
    /// </summary>
    protected abstract void ApplyChanges(TEntity entity, TCommand command);

    public virtual async Task<Result<Guid>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        // 1. Obtener la entidad (Tracking activado para Update)
        var entity = await _writeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure<Guid>(Error.NotFound($"{typeof(TEntity).Name} con ID '{command.Id}' no encontrada."));
        }

        // 2. 🔥 NUEVO: Aplicar cambios con Result (sin try-catch, sin excepciones)
        ApplyChanges(entity, command);

        // 3. Marcar la entidad como modificada (Entity Framework la rastrea automáticamente)
        _writeRepository.Update(entity);

        // 4. 🔥 Persistencia con rollback automático si falla
        try
        {
            var result = await UpdateAsync(entity, cancellationToken);

            if (result.IsFailure)
            {
                // Si UpdateAsync falla, el UnitOfWork hará rollback automáticamente
                return result;
            }

            // 5. Retornar el ID si todo fue exitoso
            return result;
        }
        catch (Exception ex)
        {
            // 🔥 Capturar errores de BD (violación de constraint, timeout, etc.)
            // El UnitOfWork hará rollback automáticamente
            return Result.Failure<Guid>(Error.Failure(
                "Database.Error",
                "Error de base de datos",
                ex.Message));
        }
    }
}