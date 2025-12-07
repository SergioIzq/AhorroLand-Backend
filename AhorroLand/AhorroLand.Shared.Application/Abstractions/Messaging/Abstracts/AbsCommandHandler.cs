using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Interfaces;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts;

/// <summary>
/// Proporciona métodos base para manejar comandos de escritura (CRUD: C, U, D) de forma asíncrona.
/// Utiliza IWriteRepository e IUnitOfWork para asegurar la segregación de responsabilidades.
/// </summary>
/// <typeparam name="TEntity">El tipo de entidad que el command handler manipula, debe heredar de AbsEntity.</typeparam>
public abstract class AbsCommandHandler<TEntity, TId> : IAbsCommandHandlerBase<TEntity, TId>
    where TEntity : AbsEntity<TId>
    where TId : IGuidValueObject
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IWriteRepository<TEntity, TId> _writeRepository;
    protected readonly ICacheService _cacheService;

    /// <summary>
    /// Inicializa una nueva instancia de la clase AbsCommandHandler.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo para gestionar la persistencia de cambios.</param>
    /// <param name="writeRepository">El repositorio con métodos de escritura (Add, Update, Delete).</param>
    public AbsCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<TEntity, TId> writeRepository,
        ICacheService cacheService
   )
    {
        _unitOfWork = unitOfWork;
        _writeRepository = writeRepository;
        _cacheService = cacheService;
    }

  // --- Métodos CUD (Create, Update, Delete) ---

    /// <summary>
    /// Añade la entidad al repositorio y persiste los cambios.
    /// </summary>
    /// <param name="entity">La entidad a crear.</param>
    /// <param name="cancellationToken">Token para monitorear peticiones de cancelación.</param>
    /// <returns>Un Result que contiene la entidad creada en caso de éxito, o Error.Conflict si falla.</returns>
    public async Task<Result<Guid>> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _writeRepository.Add(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 🔥 Invalidar caché de la entidad individual y de todas las listas/paginaciones
        await InvalidateCacheAsync(entity.Id.Value);

        return Result.Success(entity.Id.Value);

  }

    /// <summary>
    /// Marca la entidad como modificada y persiste los cambios.
    /// </summary>
    /// <param name="entity">La entidad a actualizar.</param>
    /// <param name="cancellationToken">Token para monitorear peticiones de cancelación.</param>
    /// <returns>Un Result de éxito si la actualización fue exitosa, o Error.UpdateFailure si falla.</returns>
    public async Task<Result<Guid>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
 _writeRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

 // 🔥 Invalidar caché de la entidad individual y de todas las listas/paginaciones
      await InvalidateCacheAsync(entity.Id.Value);

        return Result.Success(entity.Id.Value);
    }

  /// <summary>
    /// Marca la entidad para su eliminación y persiste los cambios.
    /// </summary>
    /// <param name="entity">La entidad a eliminar.</param>
/// <param name="cancellationToken">Token para monitorear peticiones de cancelación.</param>
    /// <returns>Un Result de éxito si la eliminación fue exitosa, o Error.DeleteFailure si falla.</returns>
    public async Task<Result> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _writeRepository.Delete(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 🔥 Invalidar caché de la entidad individual y de todas las listas/paginaciones
        await InvalidateCacheAsync(entity.Id.Value);

 return Result.Success();
    }

    /// <summary>
    /// 🔥 MEJORADO: Invalida todas las claves de caché relacionadas con la entidad.
    /// Incluye: caché individual, recent, search y cualquier lista paginada.
    /// </summary>
    protected async Task InvalidateCacheAsync(Guid id)
    {
    var entityName = typeof(TEntity).Name;
 
      // 1. Invalidar caché individual
        await _cacheService.RemoveAsync($"{entityName}:{id}");
  
        // 2. 🔥 MEJORADO: Invalidar TODAS las claves que empiecen con el nombre de la entidad
        // Esto incluye: paginaciones, búsquedas, listas, etc.
 await _cacheService.InvalidateByPatternAsync($"*{entityName}*");
    }
}