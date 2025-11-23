using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Cuentas.Queries.Search;

/// <summary>
/// Handler para búsqueda rápida de cuentas (autocomplete).
/// </summary>
public sealed class SearchCuentasQueryHandler
    : SearchForAutocompleteQueryHandler<Cuenta, CuentaDto, SearchCuentasQuery>
{
    public SearchCuentasQueryHandler(
        IReadRepositoryWithDto<Cuenta, CuentaDto> repository,
 ICacheService cacheService)
      : base(repository, cacheService)
    {
    }
}
