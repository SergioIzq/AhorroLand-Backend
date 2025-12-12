using AhorroLand.Shared.Domain.Events;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain.GastosProgramados.Eventos;

/// <summary>
/// Evento de dominio que se dispara cuando se elimina un GastoProgramado.
/// Permite cancelar el job de Hangfire correspondiente.
/// </summary>
public sealed record GastoProgramadoEliminadoEvent(
    GastoProgramadoId GastoProgramadoId,
    CuentaId CuentaId,
    Cantidad Importe
) : DomainEventBase;
