using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Proveedores.Queries;

/// <summary>
/// Representa la solicitud para crear un nuevo Proveedor.
/// </summary>
// Hereda de AbsCreateCommand<Entidad, DTO de Respuesta>
public sealed record GetProveedorByIdQuery(Guid Id) : AbsGetByIdQuery<Proveedor, ProveedorId, ProveedorDto>(Id)
{
}