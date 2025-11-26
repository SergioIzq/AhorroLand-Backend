using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.FormasPago.Queries.Recent;

public sealed class GetRecentFormasPagoQueryHandler
    : GetRecentQueryHandler<FormaPago, FormaPagoDto, FormaPagoId, GetRecentFormasPagoQuery>
{
    public GetRecentFormasPagoQueryHandler(
        IReadRepositoryWithDto<FormaPago, FormaPagoDto, FormaPagoId> repository,
        ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
