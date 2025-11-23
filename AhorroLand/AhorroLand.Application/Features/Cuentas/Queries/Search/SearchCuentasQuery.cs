using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.Cuentas.Queries.Search;

/// <summary>
/// Query para búsqueda rápida de cuentas (autocomplete).
/// </summary>
public sealed record SearchCuentasQuery : SearchForAutocompleteQuery<Cuenta, CuentaDto>
{
    public SearchCuentasQuery(string searchTerm, int limit = 10)
        : base(searchTerm, limit)
  {
    }
}
