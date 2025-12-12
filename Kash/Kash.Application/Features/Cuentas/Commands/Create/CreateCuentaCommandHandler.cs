using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Application.Abstractions.Servicies;
using Kash.Shared.Application.Interfaces;
using Kash.Shared.Domain.Abstractions.Results;
using Kash.Shared.Domain.Interfaces;
using Kash.Shared.Domain.Interfaces.Repositories;
using Kash.Shared.Domain.ValueObjects;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Cuentas.Commands;

public sealed class CreateCuentaCommandHandler : AbsCreateCommandHandler<Cuenta, CuentaId, CreateCuentaCommand>
{
    private readonly ICuentaWriteRepository _cuentaWriteRepository;

    public CreateCuentaCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Cuenta, CuentaId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        ICuentaWriteRepository cuentaWriteRepository)
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _cuentaWriteRepository = cuentaWriteRepository;
    }

    protected override Cuenta CreateEntity(CreateCuentaCommand command)
    {
        var nombreVO = Nombre.Create(command.Nombre).Value;
        var saldoInicialVO = Cantidad.Create(command.Saldo).Value;
        var usuarioId = UsuarioId.Create(command.UsuarioId).Value;

        var newCuenta = Cuenta.Create(nombreVO, saldoInicialVO, usuarioId);

        return newCuenta;
    }

    public override async Task<Result<Guid>> Handle(CreateCuentaCommand command, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad
        var entity = CreateEntity(command);

        try
        {
            // 2. Validar y agregar al contexto
            Result validationResult = await _cuentaWriteRepository.CreateAsyncWithValidation(entity, cancellationToken);

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
                "Error inesperado al crear cuenta",
                ex.Message));
        }
    }
}

