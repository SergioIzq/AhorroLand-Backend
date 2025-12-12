using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Personas.Queries.Recent;

public sealed record GetRecentPersonasQuery : GetRecentQuery<Persona, PersonaDto, PersonaId>
{
    public GetRecentPersonasQuery(int limit = 5) : base(limit) { }
}
