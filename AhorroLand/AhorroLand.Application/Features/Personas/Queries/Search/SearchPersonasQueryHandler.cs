using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Personas.Queries;

/// <summary>
/// Handler para búsqueda rápida de personas (autocomplete).
/// </summary>
public sealed class SearchPersonasQueryHandler
    : SearchForAutocompleteQueryHandler<Persona, PersonaDto, SearchPersonasQuery>
{
    public SearchPersonasQueryHandler(
        IReadRepositoryWithDto<Persona, PersonaDto> repository,
        ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
