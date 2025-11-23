using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.FormasPago.Queries.Search;

/// <summary>
/// Handler para búsqueda rápida de formas de pago (autocomplete).
/// </summary>
public sealed class SearchFormasPagoQueryHandler
    : SearchForAutocompleteQueryHandler<FormaPago, FormaPagoDto, SearchFormasPagoQuery>
{
    public SearchFormasPagoQueryHandler(
        IReadRepositoryWithDto<FormaPago, FormaPagoDto> repository,
   ICacheService cacheService)
: base(repository, cacheService)
    {
    }
}
