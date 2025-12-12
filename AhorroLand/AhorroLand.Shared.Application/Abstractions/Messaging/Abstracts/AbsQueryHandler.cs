using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Interfaces;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.Interfaces;

namespace AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts;

/// <summary>
/// Proporciona métodos base para manejar consultas de solo lectura (Queries).
/// ✅ OPTIMIZADO: Usa DTOs directamente desde IReadRepositoryWithDto.
/// 🔥 SIMPLIFICADO: Solo proporciona el cache service, los handlers concretos inyectan sus repositorios.
/// </summary>
public abstract class AbsQueryHandler<TEntity, TId> : IQueryHandlerBase<TEntity, TId>
    where TEntity : AbsEntity<TId>
    where TId : IGuidValueObject
{
    protected readonly ICacheService _cacheService;

    public AbsQueryHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }
}