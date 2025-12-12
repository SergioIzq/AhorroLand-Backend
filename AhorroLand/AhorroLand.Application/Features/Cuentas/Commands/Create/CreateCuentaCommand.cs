using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Cuentas.Commands;

public sealed record CreateCuentaCommand : AbsCreateCommand<Cuenta, CuentaId>
{
    public required string Nombre { get; init; }
    public required decimal Saldo { get; init; }
    public required Guid UsuarioId { get; init; }
}
