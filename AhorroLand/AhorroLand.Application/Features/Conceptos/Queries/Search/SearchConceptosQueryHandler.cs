using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Conceptos.Queries.Search;

/// <summary>
/// Handler para búsqueda rápida de conceptos (autocomplete).
/// </summary>
public sealed class SearchConceptosQueryHandler
    : SearchForAutocompleteQueryHandler<Concepto, ConceptoDto, SearchConceptosQuery>
{
    public SearchConceptosQueryHandler(
        IReadRepositoryWithDto<Concepto, ConceptoDto> repository,
   ICacheService cacheService)
  : base(repository, cacheService)
    {
    }
}
