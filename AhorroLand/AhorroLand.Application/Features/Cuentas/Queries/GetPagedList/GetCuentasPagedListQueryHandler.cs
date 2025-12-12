using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Cuentas.Queries;

public sealed class GetCuentasPagedListQueryHandler
    : GetPagedListQueryHandler<Cuenta, CuentaId, CuentaDto, GetCuentasPagedListQuery>
{
    public GetCuentasPagedListQueryHandler(
     IReadRepositoryWithDto<Cuenta, CuentaDto, CuentaId> repository,
  ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}