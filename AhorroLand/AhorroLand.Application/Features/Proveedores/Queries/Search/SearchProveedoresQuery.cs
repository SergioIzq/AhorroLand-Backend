using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.Proveedores.Queries.Search;

/// <summary>
/// Query para búsqueda rápida de proveedores (autocomplete).
/// </summary>
public sealed record SearchProveedoresQuery : SearchForAutocompleteQuery<Proveedor, ProveedorDto>
{
    public SearchProveedoresQuery(string searchTerm, int limit = 10)
        : base(searchTerm, limit)
    {
    }
}
