using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.FormasPago.Queries.Search;

/// <summary>
/// Query para búsqueda rápida de formas de pago (autocomplete).
/// </summary>
public sealed record SearchFormasPagoQuery : SearchForAutocompleteQuery<FormaPago, FormaPagoDto>
{
    public SearchFormasPagoQuery(string searchTerm, int limit = 10)
        : base(searchTerm, limit)
    {
    }
}
