using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Proveedores.Queries.Recent;

public sealed class GetRecentProveedoresQueryHandler
    : GetRecentQueryHandler<Proveedor, ProveedorDto, GetRecentProveedoresQuery>
{
    public GetRecentProveedoresQueryHandler(
      IReadRepositoryWithDto<Proveedor, ProveedorDto> repository,
ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
