using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Categorias.Queries.Recent;

/// <summary>
/// Query para obtener categorías recientes.
/// </summary>
public sealed record GetRecentCategoriasQuery : GetRecentQuery<Categoria, CategoriaDto, CategoriaId>
{
    public GetRecentCategoriasQuery(int limit = 5)
      : base(limit)
    {
    }
}
