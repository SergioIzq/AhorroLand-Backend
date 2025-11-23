using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.Conceptos.Queries.Recent;

public sealed record GetRecentConceptosQuery : GetRecentQuery<Concepto, ConceptoDto>
{
    public GetRecentConceptosQuery(int limit = 5) : base(limit) { }
}
