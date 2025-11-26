using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Proveedores.Commands;

/// <summary>
/// Representa la solicitud para actualizar un nuevo proveedor.
/// </summary>
// Hereda de AbsUpadteCommand<Entidad, DTO de Respuesta>
public sealed record UpdateProveedorCommand : AbsUpdateCommand<Proveedor, ProveedorId, ProveedorDto>
{
    /// <summary>
    /// Nombre del proveedor.
    /// </summary>
    public required string Nombre { get; init; }
}