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

namespace AhorroLand.Application.Features.Proveedores.Commands;

public sealed class UpdateProveedorCommandHandler
    : AbsUpdateCommandHandler<Proveedor, ProveedorId, ProveedorDto, UpdateProveedorCommand>
{
    private readonly IProveedorWriteRepository _proveedorWriteRepository;

    public UpdateProveedorCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Proveedor, ProveedorId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        IProveedorWriteRepository proveedorWriteRepository
        )
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _proveedorWriteRepository = proveedorWriteRepository;
    }

    protected override void ApplyChanges(Proveedor entity, UpdateProveedorCommand command)
    {
        var nuevoNombreVO = Nombre.Create(command.Nombre).Value;

        entity.Update(nuevoNombreVO);
    }

    public override async Task<Result<Guid>> Handle(UpdateProveedorCommand command, CancellationToken cancellationToken)
    {
        // 1. Obtener la entidad
        var entity = await _writeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure<Guid>(Error.NotFound($"{typeof(Proveedor).Name} con ID '{command.Id}' no encontrada."));
        }

        // 2. Aplicar cambios
        ApplyChanges(entity, command);

        try
        {
            // 3. Validar duplicados
            Result validationResult = await _proveedorWriteRepository.UpdateAsync(entity, cancellationToken);

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
