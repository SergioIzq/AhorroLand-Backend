using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Proveedores.Queries.Recent;

public sealed record GetRecentProveedoresQuery : GetRecentQuery<Proveedor, ProveedorDto, ProveedorId>
{
    public GetRecentProveedoresQuery(int limit = 5) : base(limit) { }
}
