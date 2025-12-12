using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Conceptos.Queries.Search;

public sealed class SearchConceptosQueryHandler
    : SearchForAutocompleteQueryHandler<Concepto, ConceptoDto, SearchConceptosQuery, ConceptoId>
{
    public SearchConceptosQueryHandler(
        IReadRepositoryWithDto<Concepto, ConceptoDto, ConceptoId> repository,
        ICacheService cacheService)
    : base(repository, cacheService)
    {
    }

    // 🔥 Sobrescribimos el Hook para inyectar el filtro de categoría
    protected override Dictionary<string, object>? GetCustomFilters(SearchConceptosQuery query)
    {
        if (string.IsNullOrEmpty(query.CategoriaId))
        {
            return null;
        }

        // Usamos el alias 'c' porque tu ConceptoReadRepository define GetTableAlias() => "c"
        return new Dictionary<string, object>
        {
            { "c.id_categoria", query.CategoriaId }
        };
    }
}