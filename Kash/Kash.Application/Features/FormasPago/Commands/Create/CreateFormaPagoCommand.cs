using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.FormasPago.Commands;

public sealed record CreateFormaPagoCommand : AbsCreateCommand<FormaPago, FormaPagoId>
{
    public required string Nombre { get; init; }
    public required Guid UsuarioId { get; init; }
}
