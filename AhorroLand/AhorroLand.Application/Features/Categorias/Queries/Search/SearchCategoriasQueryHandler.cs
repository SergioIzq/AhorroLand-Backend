using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Categorias.Queries;

/// <summary>
/// Handler para búsqueda rápida de clientes (autocomplete).
/// </summary>
public sealed class SearchCategoriasQueryHandler
    : SearchForAutocompleteQueryHandler<Categoria, CategoriaDto, SearchCategoriasQuery, CategoriaId>
{
    public SearchCategoriasQueryHandler(
        IReadRepositoryWithDto<Categoria, CategoriaDto, CategoriaId> repository,
   ICacheService cacheService)
  : base(repository, cacheService)
    {
    }
}
