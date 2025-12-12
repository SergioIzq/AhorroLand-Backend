using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.IngresosProgramados.Queries;

public sealed record GetIngresoProgramadoByIdQuery(Guid Id) : AbsGetByIdQuery<IngresoProgramado, IngresoProgramadoId, IngresoProgramadoDto>(Id)
{
}
