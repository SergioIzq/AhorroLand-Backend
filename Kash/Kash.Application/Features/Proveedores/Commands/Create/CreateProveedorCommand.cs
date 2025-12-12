using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Proveedores.Commands;

public sealed record CreateProveedorCommand : AbsCreateCommand<Proveedor, ProveedorId>
{
    public required string Nombre { get; init; }
    public required Guid UsuarioId { get; init; }
}
