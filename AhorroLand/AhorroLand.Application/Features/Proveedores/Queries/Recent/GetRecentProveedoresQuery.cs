using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.Proveedores.Queries.Recent;

public sealed record GetRecentProveedoresQuery : GetRecentQuery<Proveedor, ProveedorDto>
{
    public GetRecentProveedoresQuery(int limit = 5) : base(limit) { }
}
