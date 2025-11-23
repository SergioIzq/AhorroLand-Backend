using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Personas.Queries.Recent;

public sealed class GetRecentPersonasQueryHandler
    : GetRecentQueryHandler<Persona, PersonaDto, GetRecentPersonasQuery>
{
    public GetRecentPersonasQueryHandler(
 IReadRepositoryWithDto<Persona, PersonaDto> repository,
    ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
