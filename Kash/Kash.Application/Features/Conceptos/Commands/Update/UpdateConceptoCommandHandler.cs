using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Application.Abstractions.Servicies;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Application.Interfaces;
using Kash.Shared.Domain.Abstractions.Results;
using Kash.Shared.Domain.Interfaces;
using Kash.Shared.Domain.Interfaces.Repositories;
using Kash.Shared.Domain.ValueObjects;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Conceptos.Commands;

public sealed class UpdateConceptoCommandHandler
    : AbsUpdateCommandHandler<Concepto, ConceptoId, ConceptoDto, UpdateConceptoCommand>
{
    private readonly IConceptoWriteRepository _conceptoWriteRepository;

    public UpdateConceptoCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Concepto, ConceptoId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        IConceptoWriteRepository conceptoWriteRepository
        )
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _conceptoWriteRepository = conceptoWriteRepository;
    }

    protected override void ApplyChanges(Concepto entity, UpdateConceptoCommand command)
    {
        var nuevoNombreVO = Nombre.Create(command.Nombre).Value;
        var categoriaIdVO = CategoriaId.Create(command.CategoriaId).Value;

        entity.Update(
            nuevoNombreVO,
            categoriaIdVO
        );
    }

    public override async Task<Result<Guid>> Handle(UpdateConceptoCommand command, CancellationToken cancellationToken)
    {
        // 1. Obtener la entidad
        var entity = await _writeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure<Guid>(Error.NotFound($"{typeof(Concepto).Name} con ID '{command.Id}' no encontrada."));
        }

        // 2. Aplicar cambios
        ApplyChanges(entity, command);

        try
        {
            // 3. Validar duplicados
            Result validationResult = await _conceptoWriteRepository.UpdateAsync(entity, cancellationToken);

            if (validationResult.IsFailure)
            {
                return Result.Failure<Guid>(validationResult.Error);
            }

            // 4. Guardar cambios
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 5. Retornar el ID
            return Result.Success(entity.Id.Value);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(Error.Failure(
                "Database.Error",
                "Error de base de datos",
                ex.Message));
        }
    }
}
