using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Personas.Commands;

/// <summary>
/// Representa la solicitud para eliminar una Persona por su identificador.
/// </summary>
// Hereda de AbsDeleteCommand<Entidad>
public sealed record DeletePersonaCommand(Guid Id)
    : AbsDeleteCommand<Persona, PersonaId>(Id);