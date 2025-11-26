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
}
