using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.Personas.Queries.Recent;

public sealed record GetRecentPersonasQuery : GetRecentQuery<Persona, PersonaDto>
{
    public GetRecentPersonasQuery(int limit = 5) : base(limit) { }
}
