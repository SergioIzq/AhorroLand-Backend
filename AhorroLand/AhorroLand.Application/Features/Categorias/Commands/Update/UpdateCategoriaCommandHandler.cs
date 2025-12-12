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

namespace AhorroLand.Application.Features.Categorias.Commands;

/// <summary>
/// Maneja la creación de una nueva entidad Categoria.
/// </summary>
public sealed class UpdateCategoriaCommandHandler
    : AbsUpdateCommandHandler<Categoria, CategoriaId, CategoriaDto, UpdateCategoriaCommand>
{
    private readonly ICategoriaWriteRepository _categoriaWriteRepository;
    public UpdateCategoriaCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Categoria, CategoriaId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        ICategoriaWriteRepository categoriaWriteRepository)
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _categoriaWriteRepository = categoriaWriteRepository;
    }

    protected override void ApplyChanges(Categoria entity, UpdateCategoriaCommand command)
    {
        // 1. Crear el Value Object 'Nombre' a partir del string del comando.
        // Esto automáticamente ejecuta las reglas de validación del nombre.
        var nuevoNombreVO = Nombre.Create(command.Nombre).Value;
        var nuevADescVO = new Descripcion(command.Descripcion ?? string.Empty);

        // 2. Ejecutar el método de dominio para actualizar la entidad.
        // **La entidad (Categoria) es responsable de su propia actualización.**
        entity.Update(
            nuevoNombreVO,
            nuevADescVO
        );
    }

    public override async Task<Result<Guid>> Handle(UpdateCategoriaCommand command, CancellationToken cancellationToken)
    {
        // 1. Obtener la entidad
        var entity = await _writeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure<Guid>(Error.NotFound($"{typeof(Categoria).Name} con ID '{command.Id}' no encontrada."));
        }

        // 2. Aplicar cambios
        ApplyChanges(entity, command);

        try
        {
            // 3. Validar duplicados
            Result validationResult = await _categoriaWriteRepository.UpdateAsync(entity, cancellationToken);

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
