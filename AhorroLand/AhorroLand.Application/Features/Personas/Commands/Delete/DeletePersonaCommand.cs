using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Personas.Commands;

/// <summary>
/// Representa la solicitud para eliminar una Persona por su identificador.
/// </summary>
// Hereda de AbsDeleteCommand<Entidad>
public sealed record DeletePersonaCommand(Guid Id)
    : AbsDeleteCommand<Persona, PersonaId>(Id);