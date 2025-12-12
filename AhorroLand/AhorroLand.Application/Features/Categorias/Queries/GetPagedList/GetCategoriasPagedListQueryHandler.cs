using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.Results;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Categorias.Queries;

/// <summary>
/// Manejador concreto para la consulta de lista paginada de Categorías.
/// Implementa la lógica específica de filtrado y ordenación.
/// </summary>
public sealed class GetCategoriasPagedListQueryHandler
    : GetPagedListQueryHandler<Categoria, CategoriaId, CategoriaDto, GetCategoriasPagedListQuery>
{
    public GetCategoriasPagedListQueryHandler(
        IReadRepositoryWithDto<Categoria, CategoriaDto, CategoriaId> repository,
        ICacheService cacheService)
        : base(repository, cacheService)
    {
    }

    /// <summary>
    /// 🚀 OPTIMIZADO: Usa método específico del repositorio que filtra por usuario.
    /// </summary>
    protected override async Task<PagedList<CategoriaDto>> ApplyFiltersAsync(
        GetCategoriasPagedListQuery query,
        CancellationToken cancellationToken)
    {
        // 🔥 Si tenemos UsuarioId, usar el método optimizado con filtro
        if (query.UsuarioId.HasValue)
        {
            return await _dtoRepository.GetPagedReadModelsByUserAsync(
         query.UsuarioId.Value,
                       query.Page,
              query.PageSize,
              query.SearchTerm, // searchTerm
           query.SortColumn, // sortColumn
          query.SortOrder, // sortOrder
             cancellationToken);
        }

        // Sin UsuarioId, dejamos que el handler base maneje
        return null!;
    }
}