using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Personas.Queries;

public sealed class GetPersonasPagedListQueryHandler
  : GetPagedListQueryHandler<Persona, PersonaId, PersonaDto, GetPersonasPagedListQuery>
{
    public GetPersonasPagedListQueryHandler(
        IReadRepositoryWithDto<Persona, PersonaDto, PersonaId> repository,
    ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}