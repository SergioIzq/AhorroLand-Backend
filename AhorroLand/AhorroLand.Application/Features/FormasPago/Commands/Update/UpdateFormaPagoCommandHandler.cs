using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Application.Interfaces;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.FormasPago.Commands;

/// <summary>
/// Maneja la creación de una nueva entidad FormaPago.
/// </summary>
public sealed class UpdateFormaPagoCommandHandler
    : AbsUpdateCommandHandler<FormaPago, FormaPagoId, FormaPagoDto, UpdateFormaPagoCommand>
{
    private readonly IFormaPagoWriteRepository _formaPagoWriteRepository;

    public UpdateFormaPagoCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<FormaPago, FormaPagoId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        IFormaPagoWriteRepository formaPagoWriteRepository
        )
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _formaPagoWriteRepository = formaPagoWriteRepository;
    }

    protected override void ApplyChanges(FormaPago entity, UpdateFormaPagoCommand command)
    {
        // 1. Crear el Value Object 'Nombre' a partir del string del comando.
        // Esto automáticamente ejecuta las reglas de validación del nombre.
        var nuevoNombreVO = Nombre.Create(command.Nombre).Value;

        entity.Update(
            nuevoNombreVO
        );
    }

    public override async Task<Result<Guid>> Handle(UpdateFormaPagoCommand command, CancellationToken cancellationToken)
    {
        // 1. Obtener la entidad
        var entity = await _writeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure<Guid>(Error.NotFound($"{typeof(FormaPago).Name} con ID '{command.Id}' no encontrada."));
        }

        // 2. Aplicar cambios
        ApplyChanges(entity, command);

        try
        {
            // 3. Validar duplicados
            Result validationResult = await _formaPagoWriteRepository.UpdateAsync(entity, cancellationToken);

            if (validationResult.IsFailure)
            {
                return Result.Failure<Guid>(validationResult.Error);
            }

            // 4. Guardar cambios
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 5. Retornar el ID
            return Result.Success(entity.Id.Value);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(Error.Failure(
                "Database.Error",
                "Error de base de datos",
                ex.Message));
        }
    }
}
