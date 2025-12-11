using AhorroLand.Application.Features.Traspasos.Commands;
using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.TraspasosProgramados.Commands.Execute;

/// <summary>
/// Handler que ejecuta la lógica de negocio cuando Hangfire activa el job de un TraspasoProgramado.
/// Optimizado para minimizar queries y allocations.
/// </summary>
public sealed class ExecuteTraspasoProgramadoCommandHandler : ICommandHandler<ExecuteTraspasoProgramadoCommand>
{
    private readonly IReadRepositoryWithDto<TraspasoProgramado, TraspasoProgramadoDto, TraspasoProgramadoId> _traspasoProgramadoReadRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<ExecuteTraspasoProgramadoCommandHandler> _logger;

    public ExecuteTraspasoProgramadoCommandHandler(
        IReadRepositoryWithDto<TraspasoProgramado, TraspasoProgramadoDto, TraspasoProgramadoId> traspasoProgramadoReadRepository,
        IMediator mediator,
        ILogger<ExecuteTraspasoProgramadoCommandHandler> logger)
    {
        _traspasoProgramadoReadRepository = traspasoProgramadoReadRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Result> Handle(ExecuteTraspasoProgramadoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // ?? OPTIMIZACIÓN: Log estructurado (más eficiente que string interpolation)
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Ejecutando TraspasoProgramado {TraspasoProgramadoId}", request.TraspasoProgramadoId);
            }

            // 1. Obtener el TraspasoProgramado (AsNoTracking para mejor rendimiento)
            var traspasoProgramado = await _traspasoProgramadoReadRepository.GetReadModelByIdAsync(
                request.TraspasoProgramadoId,
                cancellationToken);

            if (traspasoProgramado == null)
            {
                _logger.LogWarning("TraspasoProgramado {TraspasoProgramadoId} no encontrado", request.TraspasoProgramadoId);
                return Result.Failure(Error.NotFound($"TraspasoProgramado con ID {request.TraspasoProgramadoId} no encontrado"));
            }

            if (!traspasoProgramado.Activo)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("TraspasoProgramado {TraspasoProgramadoId} está inactivo, se omite la ejecución", request.TraspasoProgramadoId);
                }
                return Result.Success();
            }

            // ?? OPTIMIZACIÓN: Crear el comando de forma más eficiente
            var descripcion = traspasoProgramado.Descripcion;
            var createTraspasoCommand = new CreateTraspasoCommand
            {
                Importe = traspasoProgramado.Importe,
                Fecha = DateTime.Now,
                CuentaDestinoId = traspasoProgramado.CuentaDestinoId,
                CuentaOrigenId = traspasoProgramado.CuentaOrigenId,
                UsuarioId = traspasoProgramado.UsuarioId,
                // ?? OPTIMIZACIÓN: Evitar string interpolation si no es necesario
                Descripcion = traspasoProgramado.Descripcion
            };

            var result = await _mediator.Send(createTraspasoCommand, cancellationToken);

            if (result.IsSuccess)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Traspaso creado exitosamente desde TraspasoProgramado {TraspasoProgramadoId}", request.TraspasoProgramadoId);
                }
            }
            else
            {
                _logger.LogError("Error al crear Traspaso desde TraspasoProgramado {TraspasoProgramadoId}: {Error}",
                    request.TraspasoProgramadoId, result.Error);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al ejecutar TraspasoProgramado {TraspasoProgramadoId}", request.TraspasoProgramadoId);
            return Result.Failure(Error.Failure("Execute.TraspasoProgramado", "Error de Ejecución", ex.Message));
        }
    }
}

