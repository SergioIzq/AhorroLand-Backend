using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Clientes.Queries.Recent;

public sealed record GetRecentClientesQuery : GetRecentQuery<Cliente, ClienteDto, ClienteId>
{
    public GetRecentClientesQuery(int limit = 5) : base(limit) { }
}
