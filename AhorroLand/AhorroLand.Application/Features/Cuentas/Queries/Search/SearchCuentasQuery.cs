using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Cuentas.Queries;

/// <summary>
/// Query para búsqueda rápida de cuentas (autocomplete).
/// </summary>
public sealed record SearchCuentasQuery : SearchForAutocompleteQuery<Cuenta, CuentaDto, CuentaId>
{
    public SearchCuentasQuery(string searchTerm, int limit = 10)
    : base(searchTerm, limit)
    {
    }
}
