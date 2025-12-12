using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Ingresos.Queries;

public sealed record GetIngresoByIdQuery(Guid Id) : AbsGetByIdQuery<Ingreso, IngresoId, IngresoDto>(Id)
{
}
