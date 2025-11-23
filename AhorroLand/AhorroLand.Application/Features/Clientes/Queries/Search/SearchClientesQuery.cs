using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.Clientes.Queries.Search;

/// <summary>
/// Query para búsqueda rápida de clientes (autocomplete).
/// </summary>
public sealed record SearchClientesQuery : SearchForAutocompleteQuery<Cliente, ClienteDto>
{
    public SearchClientesQuery(string searchTerm, int limit = 10)
    : base(searchTerm, limit)
    {
    }
}
