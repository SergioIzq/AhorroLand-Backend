using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Clientes.Queries;

/// <summary>
/// Representa la solicitud para crear un nuevo Cliente.
/// </summary>
// Hereda de AbsCreateCommand<Entidad, DTO de Respuesta>
public sealed record GetClienteByIdQuery(Guid Id) : AbsGetByIdQuery<Cliente, ClienteId, ClienteDto>(Id)
{
}