using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Conceptos.Queries.Recent;

public sealed class GetRecentConceptosQueryHandler
    : GetRecentQueryHandler<Concepto, ConceptoDto, ConceptoId, GetRecentConceptosQuery>
{
    public GetRecentConceptosQueryHandler(
         IReadRepositoryWithDto<Concepto, ConceptoDto, ConceptoId> repository,
        ICacheService cacheService)
        : base(repository, cacheService)
    {
    }

    protected override Dictionary<string, object>? GetCustomFilters(GetRecentConceptosQuery query)
    {
        if (string.IsNullOrEmpty(query.CategoriaId))
        {
            return null;
        }

        // Usamos "c.id_categoria" porque tu ConceptoReadRepository define el alias "c"
        // y el filtro se inyecta en el WHERE principal.
        return new Dictionary<string, object>
        {
            { "c.id_categoria", query.CategoriaId }
        };
    }

    // 🔥 Sobrescribimos para que la caché sea única por categoría
    protected override string GetCacheKeySuffix(GetRecentConceptosQuery query)
    {
        return string.IsNullOrEmpty(query.CategoriaId)
            ? string.Empty
            : $":cat_{query.CategoriaId}";
    }
}
