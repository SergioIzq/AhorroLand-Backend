using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Conceptos.Queries.Recent;

public sealed class GetRecentConceptosQueryHandler
    : GetRecentQueryHandler<Concepto, ConceptoDto, GetRecentConceptosQuery>
{
    public GetRecentConceptosQueryHandler(
 IReadRepositoryWithDto<Concepto, ConceptoDto> repository,
ICacheService cacheService)
: base(repository, cacheService)
    {
    }
}
