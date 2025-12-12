using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Proveedores.Queries;

public sealed class GetProveedoresPagedListQueryHandler
    : GetPagedListQueryHandler<Proveedor, ProveedorId, ProveedorDto, GetProveedoresPagedListQuery>
{
    public GetProveedoresPagedListQueryHandler(
        IReadRepositoryWithDto<Proveedor, ProveedorDto, ProveedorId> repository,
     ICacheService cacheService)
  : base(repository, cacheService)
    {
    }
}