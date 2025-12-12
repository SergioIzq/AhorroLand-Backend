using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Proveedores.Queries.Recent;

public sealed class GetRecentProveedoresQueryHandler
    : GetRecentQueryHandler<Proveedor, ProveedorDto, ProveedorId, GetRecentProveedoresQuery>
{
    public GetRecentProveedoresQueryHandler(
      IReadRepositoryWithDto<Proveedor, ProveedorDto, ProveedorId> repository,
ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
