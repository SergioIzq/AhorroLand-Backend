using AhorroLand.Shared.Domain.Events;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain.TraspasosProgramados.Eventos;

/// <summary>
/// Evento de dominio que se dispara cuando se elimina un TraspasoProgramado.
/// Permite cancelar el job de Hangfire correspondiente.
/// </summary>
public sealed record TraspasoProgramadoEliminadoEvent(
    TraspasoProgramadoId TraspasoProgramadoId,
    CuentaId CuentaOrigenId,
    CuentaId CuentaDestinoId,
    Cantidad Importe
) : DomainEventBase;
