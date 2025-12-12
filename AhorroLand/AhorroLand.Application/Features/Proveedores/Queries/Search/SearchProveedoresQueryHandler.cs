using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Proveedores.Queries.Search;

/// <summary>
/// Handler para búsqueda rápida de proveedores (autocomplete).
/// </summary>
public sealed class SearchProveedoresQueryHandler
    : SearchForAutocompleteQueryHandler<Proveedor, ProveedorDto, SearchProveedoresQuery, ProveedorId>
{
    public SearchProveedoresQueryHandler(
    IReadRepositoryWithDto<Proveedor, ProveedorDto, ProveedorId> repository,
        ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
