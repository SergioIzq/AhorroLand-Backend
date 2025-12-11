using AhorroLand.Application.Features.Ingresos.Commands;
using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.IngresosProgramados.Commands.Execute;

/// <summary>
/// Handler que ejecuta la lógica de negocio cuando Hangfire activa el job de un IngresoProgramado.
/// Optimizado para minimizar queries y allocations.
/// </summary>
public sealed class ExecuteIngresoProgramadoCommandHandler : ICommandHandler<ExecuteIngresoProgramadoCommand>
{
    private readonly IReadRepositoryWithDto<IngresoProgramado, IngresoProgramadoDto, IngresoProgramadoId> _gastoProgramadoReadRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<ExecuteIngresoProgramadoCommandHandler> _logger;

    public ExecuteIngresoProgramadoCommandHandler(
        IReadRepositoryWithDto<IngresoProgramado, IngresoProgramadoDto, IngresoProgramadoId> gastoProgramadoReadRepository,
        IMediator mediator,
        ILogger<ExecuteIngresoProgramadoCommandHandler> logger)
    {
        _gastoProgramadoReadRepository = gastoProgramadoReadRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Result> Handle(ExecuteIngresoProgramadoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // ?? OPTIMIZACIÓN: Log estructurado (más eficiente que string interpolation)
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Ejecutando IngresoProgramado {IngresoProgramadoId}", request.IngresoProgramadoId);
            }

            // 1. Obtener el IngresoProgramado (AsNoTracking para mejor rendimiento)
            var gastoProgramado = await _gastoProgramadoReadRepository.GetReadModelByIdAsync(
                request.IngresoProgramadoId,
                cancellationToken);

            if (gastoProgramado == null)
            {
                _logger.LogWarning("IngresoProgramado {IngresoProgramadoId} no encontrado", request.IngresoProgramadoId);
                return Result.Failure(Error.NotFound($"IngresoProgramado con ID {request.IngresoProgramadoId} no encontrado"));
            }

            if (!gastoProgramado.Activo)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("IngresoProgramado {IngresoProgramadoId} está inactivo, se omite la ejecución", request.IngresoProgramadoId);
                }
                return Result.Success();
            }

            // ?? OPTIMIZACIÓN: Crear el comando de forma más eficiente
            var descripcion = gastoProgramado.Descripcion;
            var createIngresoCommand = new CreateIngresoCommand
            {
                Importe = gastoProgramado.Importe,
                Fecha = DateTime.Now,
                ConceptoId = gastoProgramado.ConceptoId,
                CategoriaId = gastoProgramado.CategoriaId,
                ClienteId = gastoProgramado.ClienteId,
                PersonaId = gastoProgramado.PersonaId,
                CuentaId = gastoProgramado.CuentaId,
                FormaPagoId = gastoProgramado.FormaPagoId,
                UsuarioId = gastoProgramado.UsuarioId,
                // ?? OPTIMIZACIÓN: Evitar string interpolation si no es necesario
                Descripcion = gastoProgramado.Descripcion
            };

            var result = await _mediator.Send(createIngresoCommand, cancellationToken);

            if (result.IsSuccess)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Ingreso creado exitosamente desde IngresoProgramado {IngresoProgramadoId}", request.IngresoProgramadoId);
                }
            }
            else
            {
                _logger.LogError("Error al crear Ingreso desde IngresoProgramado {IngresoProgramadoId}: {Error}",
                    request.IngresoProgramadoId, result.Error);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al ejecutar IngresoProgramado {IngresoProgramadoId}", request.IngresoProgramadoId);
            return Result.Failure(Error.Failure("Execute.IngresoProgramado", "Error de Ejecución", ex.Message));
        }
    }
}

