using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Categorias.Queries;

/// <summary>
/// Handler para búsqueda rápida de clientes (autocomplete).
/// </summary>
public sealed class SearchCategoriasQueryHandler
    : SearchForAutocompleteQueryHandler<Categoria, CategoriaDto, SearchCategoriasQuery>
{
    public SearchCategoriasQueryHandler(
        IReadRepositoryWithDto<Categoria, CategoriaDto> repository,
   ICacheService cacheService)
  : base(repository, cacheService)
    {
    }
}
