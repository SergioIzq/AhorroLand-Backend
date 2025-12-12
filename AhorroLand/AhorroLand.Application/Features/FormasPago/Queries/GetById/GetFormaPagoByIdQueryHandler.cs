using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.FormasPago.Queries;

/// <summary>
/// Maneja la creación de una nueva entidad FormaPago.
/// </summary>
public sealed class GetFormaPagoByIdQueryHandler
    : GetByIdQueryHandler<FormaPago, FormaPagoId, FormaPagoDto, GetFormaPagoByIdQuery>
{
    public GetFormaPagoByIdQueryHandler(
        ICacheService cacheService,
        IReadRepositoryWithDto<FormaPago, FormaPagoDto, FormaPagoId> readOnlyRepository
        )
        : base(readOnlyRepository, cacheService)
    {
    }
}