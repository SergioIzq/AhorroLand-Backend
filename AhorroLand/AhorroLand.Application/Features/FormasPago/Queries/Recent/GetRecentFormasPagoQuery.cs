using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.FormasPago.Queries.Recent;

public sealed record GetRecentFormasPagoQuery : GetRecentQuery<FormaPago, FormaPagoDto>
{
    public GetRecentFormasPagoQuery(int limit = 5) : base(limit) { }
}
