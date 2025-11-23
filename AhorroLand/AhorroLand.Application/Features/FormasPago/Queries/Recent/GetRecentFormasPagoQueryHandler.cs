using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.FormasPago.Queries.Recent;

public sealed class GetRecentFormasPagoQueryHandler
    : GetRecentQueryHandler<FormaPago, FormaPagoDto, GetRecentFormasPagoQuery>
{
    public GetRecentFormasPagoQueryHandler(
        IReadRepositoryWithDto<FormaPago, FormaPagoDto> repository,
        ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
