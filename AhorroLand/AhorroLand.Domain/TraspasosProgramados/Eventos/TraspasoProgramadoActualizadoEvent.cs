using AhorroLand.Shared.Domain.Events;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain.TraspasosProgramados.Eventos;

/// <summary>
/// Evento de dominio que se dispara cuando se actualiza un TraspasoProgramado.
/// Permite cancelar el job anterior y reprogramar uno nuevo si cambió el importe o las cuentas.
/// </summary>
public sealed record TraspasoProgramadoActualizadoEvent(
    TraspasoProgramadoId TraspasoProgramadoId,
    CuentaId CuentaOrigenIdAnterior,
    CuentaId CuentaDestinoIdAnterior,
    Cantidad ImporteAnterior,
    CuentaId CuentaOrigenIdNueva,
    CuentaId CuentaDestinoIdNueva,
    Cantidad ImporteNuevo
) : DomainEventBase;
