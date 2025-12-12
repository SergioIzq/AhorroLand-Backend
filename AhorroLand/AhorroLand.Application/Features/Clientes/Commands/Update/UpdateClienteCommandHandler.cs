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

namespace AhorroLand.Application.Features.Clientes.Commands;

/// <summary>
/// Maneja la creación de una nueva entidad Categoria.
/// </summary>
public sealed class UpdateClienteCommandHandler
    : AbsUpdateCommandHandler<Cliente, ClienteId, ClienteDto, UpdateClienteCommand>
{
    private readonly IClienteWriteRepository _clienteWriteRepository;
    public UpdateClienteCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Cliente, ClienteId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        IClienteWriteRepository clienteWriteRepository
        )
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _clienteWriteRepository = clienteWriteRepository;
    }

    public override async Task<Result<Guid>> Handle(UpdateClienteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _writeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure<Guid>(Error.NotFound($"{typeof(Cliente).Name} con ID '{command.Id}' no encontrada."));
        }

        // 2. 🔥 NUEVO: Aplicar cambios con Result (sin try-catch, sin excepciones)
        ApplyChanges(entity, command);

        try
        {
            Result validationResult = await _clienteWriteRepository.UpdateAsync(entity, cancellationToken);

            if (validationResult.IsFailure)
            {
                // Si UpdateAsync falla, el UnitOfWork hará rollback automáticamente
                return Result.Failure<Guid>(validationResult.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 5. Retornar el ID (Éxito)
            // Como validationResult no tiene valor, sacamos el ID de la entidad original
            return Result.Success(entity.Id.Value);
        }
        catch (Exception ex)
        {
            // 🔥 Capturar errores de BD (violación de constraint, timeout, etc.)
            // El UnitOfWork hará rollback automáticamente
            return Result.Failure<Guid>(Error.Failure(
                "Database.Error",
                "Error de base de datos",
                ex.Message));
        }
    }

    protected override void ApplyChanges(Cliente entity, UpdateClienteCommand command)
    {
        // 1. Crear el Value Object 'Nombre' a partir del string del comando.
        // Esto automáticamente ejecuta las reglas de validación del nombre.
        var nuevoNombreVO = Nombre.Create(command.Nombre).Value;

        entity.Update(
            nuevoNombreVO
        );
    }
}
