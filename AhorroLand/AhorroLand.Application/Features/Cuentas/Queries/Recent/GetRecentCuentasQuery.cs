using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Cuentas.Queries.Recent;

public sealed record GetRecentCuentasQuery : GetRecentQuery<Cuenta, CuentaDto, CuentaId>
{
    public GetRecentCuentasQuery(int limit = 5) : base(limit) { }
}
