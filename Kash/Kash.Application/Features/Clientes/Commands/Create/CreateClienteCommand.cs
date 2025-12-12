using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Clientes.Commands;

/// <summary>
/// Representa la solicitud para crear una nueva Cliente.
/// </summary>
public sealed record CreateClienteCommand(string Nombre, Guid UsuarioId) : AbsCreateCommand<Cliente, ClienteId>
{
}