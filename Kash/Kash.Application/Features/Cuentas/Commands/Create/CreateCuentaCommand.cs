using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Cuentas.Commands;

public sealed record CreateCuentaCommand : AbsCreateCommand<Cuenta, CuentaId>
{
    public required string Nombre { get; init; }
    public required decimal Saldo { get; init; }
    public required Guid UsuarioId { get; init; }
}
