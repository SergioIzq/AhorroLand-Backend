using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Personas.Queries.Recent;

public sealed record GetRecentPersonasQuery : GetRecentQuery<Persona, PersonaDto, PersonaId>
{
    public GetRecentPersonasQuery(int limit = 5) : base(limit) { }
}
