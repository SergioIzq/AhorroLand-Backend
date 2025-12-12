using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Application.Abstractions.Servicies;
using Kash.Shared.Application.Interfaces;
using Kash.Shared.Domain.Abstractions.Results;
using Kash.Shared.Domain.Interfaces;
using Kash.Shared.Domain.Interfaces.Repositories;
using Kash.Shared.Domain.ValueObjects;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Conceptos.Commands;

public sealed class CreateConceptoCommandHandler
    : AbsCreateCommandHandler<Concepto, ConceptoId, CreateConceptoCommand>
{
    private readonly IConceptoWriteRepository _conceptoWriteRepository;

    public CreateConceptoCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Concepto, ConceptoId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        IConceptoWriteRepository conceptoWriteRepository)
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _conceptoWriteRepository = conceptoWriteRepository;
    }

    protected override Concepto CreateEntity(CreateConceptoCommand command)
    {
        var nombreVO = Nombre.Create(command.Nombre).Value;
        var usuarioId = UsuarioId.Create(command.UsuarioId).Value;
        var categoriaId = CategoriaId.Create(command.CategoriaId).Value;

        var newConcepto = Concepto.Create(
            nombreVO,
            categoriaId,
            usuarioId
        );

        return newConcepto;
    }

    public override async Task<Result<Guid>> Handle(CreateConceptoCommand command, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad
        var entity = CreateEntity(command);

        try
        {
            // 2. Validar y agregar al contexto
            Result validationResult = await _conceptoWriteRepository.CreateAsyncWithValidation(entity, cancellationToken);

            if (validationResult.IsFailure)
            {
                return Result.Failure<Guid>(validationResult.Error);
            }

            // 3. Guardar cambios
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 4. Retornar el ID
            return Result.Success(entity.Id.Value);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(Error.Failure(
                "Database.Error",
                "Error inesperado al crear concepto",
                ex.Message));
        }
    }
}
