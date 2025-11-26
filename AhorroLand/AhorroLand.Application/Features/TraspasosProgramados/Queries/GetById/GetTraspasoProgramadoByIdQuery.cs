using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.TraspasosProgramados.Queries;

public sealed record GetTraspasoProgramadoByIdQuery(Guid Id) : AbsGetByIdQuery<TraspasoProgramado, TraspasoProgramadoId, TraspasoProgramadoDto>(Id)
{
}
