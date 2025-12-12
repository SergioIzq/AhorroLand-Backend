using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Interfaces;
using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using MediatR;

namespace AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;

/// <summary>
/// Handler genérico para crear entidades. 
/// 🔥 MODIFICADO: Maneja validaciones de dominio con Result en lugar de excepciones.
/// 🔥 ROLLBACK AUTOMÁTICO: Si CreateEntity falla, no se persiste nada.
/// </summary>
public abstract class AbsCreateCommandHandler<TEntity, TId, TCommand>
    : AbsCommandHandler<TEntity, TId>, IRequestHandler<TCommand, Result<Guid>>
    where TEntity : AbsEntity<TId>
    where TCommand : AbsCreateCommand<TEntity, TId>
    where TId : IGuidValueObject
{
    protected AbsCreateCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<TEntity, TId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext)
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
    }

    /// <summary>
    /// Método abstracto: El hijo debe saber cómo instanciar la entidad.
    /// 🔥 CAMBIO IMPORTANTE: Ahora devuelve Result<TEntity> en lugar de TEntity directo.
    /// </summary>
    protected abstract TEntity CreateEntity(TCommand command);

    public virtual async Task<Result<Guid>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        // 1. 🔥 NUEVO: Crear entidad con Result (sin try-catch, sin excepciones)
        var entity = CreateEntity(command);

        // 2. 🔥 Persistencia con rollback automático si falla
        try
        {
            var result = await CreateAsync(entity, cancellationToken);

            if (result.IsFailure)
            {
                // Si CreateAsync falla, el UnitOfWork hará rollback automáticamente
                return result;
            }

            // 3. Retornar el ID si todo fue exitoso
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