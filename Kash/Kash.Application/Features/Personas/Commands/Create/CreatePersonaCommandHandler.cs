using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Application.Abstractions.Servicies;
using Kash.Shared.Application.Interfaces;
using Kash.Shared.Domain.Interfaces;
using Kash.Shared.Domain.Interfaces.Repositories;
using Kash.Shared.Domain.ValueObjects;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Personas.Commands;

public sealed class CreatePersonaCommandHandler : AbsCreateCommandHandler<Persona, PersonaId, CreatePersonaCommand>
{
    public CreatePersonaCommandHandler(
    IUnitOfWork unitOfWork,
    IWriteRepository<Persona, PersonaId> writeRepository,
    ICacheService cacheService,
    IUserContext userContext)
    : base(unitOfWork, writeRepository, cacheService, userContext)
    {
    }

    protected override Persona CreateEntity(CreatePersonaCommand command)
    {
        var nombreVO = Nombre.Create(command.Nombre).Value;
        var usuarioId = UsuarioId.Create(command.UsuarioId).Value;

        var newPersona = Persona.Create(Guid.NewGuid(), nombreVO, usuarioId);
        return newPersona;
    }
}

