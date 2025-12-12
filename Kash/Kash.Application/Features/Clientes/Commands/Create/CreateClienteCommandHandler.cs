using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Application.Abstractions.Servicies;
using Kash.Shared.Application.Interfaces;
using Kash.Shared.Domain.Abstractions.Results;
using Kash.Shared.Domain.Interfaces;
using Kash.Shared.Domain.Interfaces.Repositories;
using Kash.Shared.Domain.ValueObjects;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Clientes.Commands;

/// <summary>
/// Maneja la creación de una nueva entidad Cliente.
/// </summary>
public sealed class CreateClienteCommandHandler
    : AbsCreateCommandHandler<Cliente, ClienteId, CreateClienteCommand>
{
    private readonly IClienteWriteRepository _clienteWriteRepository;

    public CreateClienteCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Cliente, ClienteId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        IClienteWriteRepository clienteWriteRepository)
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _clienteWriteRepository = clienteWriteRepository;
    }

    /// <summary>
    /// **Implementación de la lógica de negocio**: Crea la entidad Cliente.
    /// Este es el único método que tienes que implementar y donde se aplica el DDD.
    /// </summary>
    /// <param name="command">El comando con los datos de creación.</param>
    /// <returns>La nueva entidad Cliente creada.</returns>
    protected override Cliente CreateEntity(CreateClienteCommand command)
    {
        var nombreVO = Nombre.Create(command.Nombre).Value;
        var usuarioIdVO = UsuarioId.Create(command.UsuarioId).Value;

        var newCliente = Cliente.Create(nombreVO, usuarioIdVO);

        return newCliente;
    }

    public async override Task<Result<Guid>> Handle(CreateClienteCommand command, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad (El ID se genera aquí, ya sea en el constructor o factory)
        var entity = CreateEntity(command);

        try
        {
            // 2. Ejecutar la validación y añadir al contexto
            // Esto devuelve Result (sin valor)
            Result validationResult = await _clienteWriteRepository.CreateAsyncWithValidation(entity, cancellationToken);

            // 3. Verificar fallo y convertir tipo
            if (validationResult.IsFailure)
            {
                // 🔥 CORRECCIÓN AQUÍ:
                // Convertimos el error del Result simple a un Result<Guid>
                return Result.Failure<Guid>(validationResult.Error);
            }

            // 4. 🔥 IMPORTANTE: Guardar cambios (Unit of Work)
            // Si tu repositorio no hace SaveChanges internamente (que es lo correcto en UoW),
            // debes llamar al UnitOfWork aquí para persistir la transacción.
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 5. Retornar el ID (Éxito)
            // Como validationResult no tiene valor, sacamos el ID de la entidad original
            return Result.Success(entity.Id.Value);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(Error.Failure(
                "Database.Error",
                "Error inesperado al crear cliente",
                ex.Message));
        }
    }
}
