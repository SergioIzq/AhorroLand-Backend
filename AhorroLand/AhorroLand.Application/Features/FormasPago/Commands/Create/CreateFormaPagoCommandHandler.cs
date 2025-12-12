using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Interfaces;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.FormasPago.Commands;

public sealed class CreateFormaPagoCommandHandler : AbsCreateCommandHandler<FormaPago, FormaPagoId, CreateFormaPagoCommand>
{
    private readonly IFormaPagoWriteRepository _formaPagoWriteRepository;

    public CreateFormaPagoCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<FormaPago, FormaPagoId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        IFormaPagoWriteRepository formaPagoWriteRepository)
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _formaPagoWriteRepository = formaPagoWriteRepository;
    }

    protected override FormaPago CreateEntity(CreateFormaPagoCommand command)
    {
        var nombreVO = Nombre.Create(command.Nombre).Value;
        var usuarioId = UsuarioId.Create(command.UsuarioId).Value;

        var newFormaPago = FormaPago.Create(nombreVO, usuarioId);

        return newFormaPago;
    }

    public override async Task<Result<Guid>> Handle(CreateFormaPagoCommand command, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad
        var entity = CreateEntity(command);

        try
        {
            // 2. Validar y agregar al contexto
            Result validationResult = await _formaPagoWriteRepository.CreateAsyncWithValidation(entity, cancellationToken);

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
                "Error inesperado al crear forma de pago",
                ex.Message));
        }
    }
}

