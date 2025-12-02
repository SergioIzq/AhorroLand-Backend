using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using MediatR;

namespace AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;

/// <summary>
/// Handler base para obtener elementos recientes ultra-rápidos.
/// ? Con cache de corta duración (30 segundos) para mejorar performance
/// ? Resultados limitados (máximo 5-50 items)
/// ? Optimizado para <10ms de respuesta
/// </summary>
public abstract class GetRecentQueryHandler<TEntity, TDto, TId, TQuery>
    : AbsQueryHandler<TEntity, TId>, IRequestHandler<TQuery, Result<IEnumerable<TDto>>>
    where TEntity : AbsEntity<TId>
    where TQuery : GetRecentQuery<TEntity, TDto, TId>
    where TDto : class
    where TId : IGuidValueObject
{
    protected readonly IReadRepositoryWithDto<TEntity, TDto, TId> _repository;

    protected GetRecentQueryHandler(
        IReadRepositoryWithDto<TEntity, TDto, TId> repository,
        ICacheService cacheService)
        : base(cacheService)
    {
        _repository = repository;
    }

    public virtual async Task<Result<IEnumerable<TDto>>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        // Validación: debe tener usuarioId
        if (!query.UsuarioId.HasValue)
        {
            return Result.Failure<IEnumerable<TDto>>(
             Error.Validation("El ID de usuario es requerido."));
        }

        // ?? CACHE: Intentar obtener del cache (reduce carga en BD)
        string cacheKey = $"{typeof(TEntity).Name}:recent:{query.UsuarioId}:{query.Limit}";
        var cachedResult = await _cacheService.GetAsync<IEnumerable<TDto>>(cacheKey);

        if (cachedResult != null)
        {
            return Result.Success(cachedResult);
        }

        // ?? Búsqueda rápida de elementos recientes
        var results = await _repository.GetRecentAsync(
            query.UsuarioId.Value,
         query.Limit,
   cancellationToken);

        // ?? CACHE: Guardar en cache por 30 segundos (datos que cambian con frecuencia)
        await _cacheService.SetAsync(
     cacheKey,
         results,
              slidingExpiration: TimeSpan.FromSeconds(30));

        return Result.Success(results);
    }
}
