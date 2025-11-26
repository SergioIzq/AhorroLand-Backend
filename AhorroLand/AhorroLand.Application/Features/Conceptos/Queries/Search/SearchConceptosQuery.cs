using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Conceptos.Queries.Search;

/// <summary>
/// Query para búsqueda rápida de conceptos (autocomplete).
/// </summary>
public sealed record SearchConceptosQuery : SearchForAutocompleteQuery<Concepto, ConceptoDto, ConceptoId>
{
    public SearchConceptosQuery(string searchTerm, int limit = 10)
    : base(searchTerm, limit)
    {
    }
}
