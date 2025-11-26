using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Cuentas.Queries.Recent;

public sealed class GetRecentCuentasQueryHandler
    : GetRecentQueryHandler<Cuenta, CuentaDto, CuentaId, GetRecentCuentasQuery>
{
    public GetRecentCuentasQueryHandler(
   IReadRepositoryWithDto<Cuenta, CuentaDto, CuentaId> repository,
        ICacheService cacheService)
      : base(repository, cacheService)
    {
    }
}
