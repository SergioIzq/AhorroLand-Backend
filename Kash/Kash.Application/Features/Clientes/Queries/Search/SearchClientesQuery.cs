using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Clientes.Queries.Search;

/// <summary>
/// Query para búsqueda rápida de clientes (autocomplete).
/// </summary>
public sealed record SearchClientesQuery : SearchForAutocompleteQuery<Cliente, ClienteDto, ClienteId>
{
    public SearchClientesQuery(string searchTerm, int limit = 10)
    : base(searchTerm, limit)
    {
    }
}
