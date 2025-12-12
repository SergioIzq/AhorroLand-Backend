using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Application.Interfaces;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Cuentas.Commands;

public sealed class UpdateCuentaCommandHandler
    : AbsUpdateCommandHandler<Cuenta, CuentaId, CuentaDto, UpdateCuentaCommand>
{
    private readonly ICuentaWriteRepository _cuentaWriteRepository;

    public UpdateCuentaCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Cuenta, CuentaId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        ICuentaWriteRepository cuentaWriteRepository
        )
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _cuentaWriteRepository = cuentaWriteRepository;
    }

    protected override void ApplyChanges(Cuenta entity, UpdateCuentaCommand command)
    {
        var nuevoNombreVO = Nombre.Create(command.Nombre).Value;

        entity.Update(nuevoNombreVO);
    }

    public override async Task<Result<Guid>> Handle(UpdateCuentaCommand command, CancellationToken cancellationToken)
    {
        // 1. Obtener la entidad
        var entity = await _writeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure<Guid>(Error.NotFound($"{typeof(Cuenta).Name} con ID '{command.Id}' no encontrada."));
        }

        // 2. Aplicar cambios
        ApplyChanges(entity, command);

        try
        {
            // 3. Validar duplicados
            Result validationResult = await _cuentaWriteRepository.UpdateAsync(entity, cancellationToken);

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
