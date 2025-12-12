using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Clientes.Queries.Recent;

public sealed record GetRecentClientesQuery : GetRecentQuery<Cliente, ClienteDto, ClienteId>
{
    public GetRecentClientesQuery(int limit = 5) : base(limit) { }
}
