using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Proveedores.Queries.Search;

/// <summary>
/// Query para búsqueda rápida de proveedores (autocomplete).
/// </summary>
public sealed record SearchProveedoresQuery : SearchForAutocompleteQuery<Proveedor, ProveedorDto, ProveedorId>
{
    public SearchProveedoresQuery(string searchTerm, int limit = 10)
        : base(searchTerm, limit)
    {
    }
}
