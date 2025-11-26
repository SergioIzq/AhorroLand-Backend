using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Proveedores.Commands;

public sealed class CreateProveedorCommandHandler : AbsCreateCommandHandler<Proveedor, ProveedorId, CreateProveedorCommand>
{
    public CreateProveedorCommandHandler(
    IUnitOfWork unitOfWork,
    IWriteRepository<Proveedor, ProveedorId> writeRepository,
    ICacheService cacheService)
    : base(unitOfWork, writeRepository, cacheService)
    {
    }

    protected override Proveedor CreateEntity(CreateProveedorCommand command)
    {
        var nombreVO = new Nombre(command.Nombre);
        var usuarioId = new UsuarioId(command.UsuarioId);


        var newProveedor = Proveedor.Create(Guid.NewGuid(), nombreVO, usuarioId);
        return newProveedor;
    }
}

