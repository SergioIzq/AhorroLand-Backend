using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Ingresos.Queries;

public sealed record GetIngresoByIdQuery(Guid Id) : AbsGetByIdQuery<Ingreso, IngresoId, IngresoDto>(Id)
{
}
