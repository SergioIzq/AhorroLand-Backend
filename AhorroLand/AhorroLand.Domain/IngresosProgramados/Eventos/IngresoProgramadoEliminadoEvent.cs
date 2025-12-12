using AhorroLand.Shared.Domain.Events;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain.IngresosProgramados.Eventos;

/// <summary>
/// Evento de dominio que se dispara cuando se elimina un IngresoProgramado.
/// Permite cancelar el job de Hangfire correspondiente.
/// </summary>
public sealed record IngresoProgramadoEliminadoEvent(
    IngresoProgramadoId IngresoProgramadoId,
    CuentaId CuentaId,
    Cantidad Importe
) : DomainEventBase;
