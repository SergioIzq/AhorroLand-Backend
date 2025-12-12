using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Personas.Commands;

public sealed record CreatePersonaCommand : AbsCreateCommand<Persona, PersonaId>
{
    public required string Nombre { get; init; }
    public required Guid UsuarioId { get; init; }
}
