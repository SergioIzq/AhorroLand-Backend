using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Cuentas.Queries.Recent;

public sealed record GetRecentCuentasQuery : GetRecentQuery<Cuenta, CuentaDto, CuentaId>
{
    public GetRecentCuentasQuery(int limit = 5) : base(limit) { }
}
