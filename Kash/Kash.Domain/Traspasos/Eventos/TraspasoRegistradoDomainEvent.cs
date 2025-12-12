using Kash.Shared.Domain.Interfaces;
using Kash.Shared.Domain.ValueObjects;

namespace Kash.Domain.Traspasos.Eventos
{
    public sealed record TraspasoRegistradoDomainEvent(Guid TraspasoId, Guid CuentaOrigenId, Guid CuentaDestinoId, Cantidad Importe) : IDomainEvent;
}
