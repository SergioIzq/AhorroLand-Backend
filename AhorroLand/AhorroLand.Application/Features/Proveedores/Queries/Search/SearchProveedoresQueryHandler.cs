using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Proveedores.Queries.Search;

/// <summary>
/// Handler para búsqueda rápida de proveedores (autocomplete).
/// </summary>
public sealed class SearchProveedoresQueryHandler
    : SearchForAutocompleteQueryHandler<Proveedor, ProveedorDto, SearchProveedoresQuery>
{
    public SearchProveedoresQueryHandler(
    IReadRepositoryWithDto<Proveedor, ProveedorDto> repository,
        ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
