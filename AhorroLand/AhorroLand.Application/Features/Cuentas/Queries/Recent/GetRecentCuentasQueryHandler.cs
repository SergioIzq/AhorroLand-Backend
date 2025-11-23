using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Cuentas.Queries.Recent;

public sealed class GetRecentCuentasQueryHandler
    : GetRecentQueryHandler<Cuenta, CuentaDto, GetRecentCuentasQuery>
{
    public GetRecentCuentasQueryHandler(
   IReadRepositoryWithDto<Cuenta, CuentaDto> repository,
        ICacheService cacheService)
      : base(repository, cacheService)
    {
    }
}
