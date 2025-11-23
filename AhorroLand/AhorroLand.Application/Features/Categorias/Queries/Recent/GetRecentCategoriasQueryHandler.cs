using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Categorias.Queries.Recent;

/// <summary>
/// Handler para obtener categorías recientes.
/// </summary>
public sealed class GetRecentCategoriasQueryHandler
    : GetRecentQueryHandler<Categoria, CategoriaDto, GetRecentCategoriasQuery>
{
    public GetRecentCategoriasQueryHandler(
        IReadRepositoryWithDto<Categoria, CategoriaDto> repository,
      ICacheService cacheService)
      : base(repository, cacheService)
    {
    }
}
