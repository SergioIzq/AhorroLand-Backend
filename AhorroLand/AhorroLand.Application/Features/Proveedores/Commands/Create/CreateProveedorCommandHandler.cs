using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Interfaces;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Proveedores.Commands;

public sealed class CreateProveedorCommandHandler : AbsCreateCommandHandler<Proveedor, ProveedorId, CreateProveedorCommand>
{
    private readonly IProveedorWriteRepository _proveedorWriteRepository;

    public CreateProveedorCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Proveedor, ProveedorId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        IProveedorWriteRepository proveedorWriteRepository)
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _proveedorWriteRepository = proveedorWriteRepository;
    }

    protected override Proveedor CreateEntity(CreateProveedorCommand command)
    {
        var nombreVO = Nombre.Create(command.Nombre).Value;
        var usuarioId = UsuarioId.Create(command.UsuarioId).Value;

        var newProveedor = Proveedor.Create(Guid.NewGuid(), nombreVO, usuarioId);

        return newProveedor;
    }

    public override async Task<Result<Guid>> Handle(CreateProveedorCommand command, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad
        var entity = CreateEntity(command);

        try
        {
            // 2. Validar y agregar al contexto
            Result validationResult = await _proveedorWriteRepository.CreateAsyncWithValidation(entity, cancellationToken);

            if (validationResult.IsFailure)
            {
                return Result.Failure<Guid>(validationResult.Error);
            }

            // 3. Guardar cambios
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 4. Retornar el ID
            return Result.Success(entity.Id.Value);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(Error.Failure(
                "Database.Error",
                "Error inesperado al crear proveedor",
                ex.Message));
        }
    }
}

