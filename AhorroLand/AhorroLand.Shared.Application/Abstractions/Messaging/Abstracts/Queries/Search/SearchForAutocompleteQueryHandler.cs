using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using MediatR;

namespace AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;

/// <summary>
/// Handler base para búsquedas de autocomplete ultra-rápidas.
/// ? Sin cache (los datos cambian frecuentemente y son ligeros)
/// ? Resultados limitados (máximo 10-50 items)
/// ? Optimizado para <10ms de respuesta
/// </summary>
public abstract class SearchForAutocompleteQueryHandler<TEntity, TDto, TQuery>
    : AbsQueryHandler<TEntity>, IRequestHandler<TQuery, Result<IEnumerable<TDto>>>
    where TEntity : AbsEntity
    where TQuery : SearchForAutocompleteQuery<TEntity, TDto>
    where TDto : class
{
    protected readonly IReadRepositoryWithDto<TEntity, TDto> _repository;

    protected SearchForAutocompleteQueryHandler(
  IReadRepositoryWithDto<TEntity, TDto> repository,
  ICacheService cacheService)
        : base(cacheService)
    {
        _repository = repository;
    }

    public virtual async Task<Result<IEnumerable<TDto>>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        // Validación: el término de búsqueda no puede estar vacío
        if (string.IsNullOrWhiteSpace(query.SearchTerm))
      {
       return Result.Success(Enumerable.Empty<TDto>());
        }

        // Validación: debe tener usuarioId
        if (!query.UsuarioId.HasValue)
        {
  return Result.Failure<IEnumerable<TDto>>(
      Error.Validation("El ID de usuario es requerido para la búsqueda."));
    }

        // ?? Búsqueda ultra-rápida sin cache (datos ligeros y dinámicos)
 var results = await _repository.SearchForAutocompleteAsync(
            query.UsuarioId.Value,
    query.SearchTerm,
    query.Limit,
      cancellationToken);

        return Result.Success(results);
    }
}
