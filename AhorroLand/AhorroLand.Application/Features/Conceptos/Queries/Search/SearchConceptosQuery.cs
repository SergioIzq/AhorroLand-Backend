using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.Conceptos.Queries.Search;

/// <summary>
/// Query para búsqueda rápida de conceptos (autocomplete).
/// </summary>
public sealed record SearchConceptosQuery : SearchForAutocompleteQuery<Concepto, ConceptoDto>
{
    public SearchConceptosQuery(string searchTerm, int limit = 10)
    : base(searchTerm, limit)
    {
    }
}
