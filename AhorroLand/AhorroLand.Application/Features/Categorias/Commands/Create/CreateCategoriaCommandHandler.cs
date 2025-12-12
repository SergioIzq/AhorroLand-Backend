using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Abstractions.Servicies;
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
public sealed class CreateCategoriaCommandHandler
    : AbsCreateCommandHandler<Categoria, CategoriaId, CreateCategoriaCommand>
{
    private readonly ICategoriaWriteRepository _categoriaWriteRepository;
    public CreateCategoriaCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Categoria, CategoriaId> writeRepository,
        ICacheService cacheService,
        IUserContext userContext,
        ICategoriaWriteRepository categoriaWriteRepository)
        : base(unitOfWork, writeRepository, cacheService, userContext)
    {
        _categoriaWriteRepository = categoriaWriteRepository;
    }

    /// <summary>
    /// **Implementación de la lógica de negocio**: Crea la entidad Categoria.
    /// Este es el único método que tienes que implementar y donde se aplica el DDD.
    /// </summary>
    /// <param name="command">El comando con los datos de creación.</param>
    /// <returns>La nueva entidad Categoria creada.</returns>
    protected override Categoria CreateEntity(CreateCategoriaCommand command)
    {
        var nombreVO = Nombre.Create(command.Nombre).Value;
        var descripcionVO = new Descripcion(command.Descripcion ?? string.Empty);
        var usuarioId = UsuarioId.Create(command.UsuarioId).Value;

        var newCategoria = Categoria.Create(
            nombreVO,
            usuarioId,
            descripcionVO
        );

        return newCategoria;
    }

    public override async Task<Result<Guid>> Handle(CreateCategoriaCommand command, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad
        var entity = CreateEntity(command);

        try
        {
            // 2. Validar y agregar al contexto
            Result validationResult = await _categoriaWriteRepository.CreateAsyncWithValidation(entity, cancellationToken);

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
                "Error inesperado al crear categoría",
                ex.Message));
        }
    }
}
