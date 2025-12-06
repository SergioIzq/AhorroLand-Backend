using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Conceptos.Queries.Search;

// 1. Añadimos la propiedad CategoriaId aquí
public sealed record SearchConceptosQuery(string SearchTerm, int Limit = 10)
    : SearchForAutocompleteQuery<Concepto, ConceptoDto, ConceptoId>(SearchTerm, Limit)
{
    public string? CategoriaId { get; init; }
}