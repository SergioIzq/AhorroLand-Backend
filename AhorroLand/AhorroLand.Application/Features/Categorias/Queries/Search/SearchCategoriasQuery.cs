using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.Categorias.Queries;

/// <summary>
/// Query para búsqueda rápida de clientes (autocomplete).
/// </summary>
public sealed record SearchCategoriasQuery : SearchForAutocompleteQuery<Categoria, CategoriaDto>
{
    public SearchCategoriasQuery(string searchTerm, int limit = 10)
    : base(searchTerm, limit)
    {
    }
}
