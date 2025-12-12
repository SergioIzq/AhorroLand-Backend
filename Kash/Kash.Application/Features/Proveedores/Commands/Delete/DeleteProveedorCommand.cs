using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Proveedores.Commands;

/// <summary>
/// Representa la solicitud para eliminar una Proveedor por su identificador.
/// </summary>
// Hereda de AbsDeleteCommand<Entidad>
public sealed record DeleteProveedorCommand(Guid Id)
    : AbsDeleteCommand<Proveedor, ProveedorId>(Id);