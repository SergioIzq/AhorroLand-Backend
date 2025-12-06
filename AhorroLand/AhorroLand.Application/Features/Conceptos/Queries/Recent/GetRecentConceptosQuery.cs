using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Conceptos.Queries.Recent;

public sealed record GetRecentConceptosQuery : GetRecentQuery<Concepto, ConceptoDto, ConceptoId>
{
    public GetRecentConceptosQuery(int limit = 5, string? categoriaId = null) : base(limit)
    {
        CategoriaId = categoriaId;
    }

    public string? CategoriaId { get; }
}
