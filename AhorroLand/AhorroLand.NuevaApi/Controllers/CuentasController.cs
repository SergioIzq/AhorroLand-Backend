using AhorroLand.Application.Features.Cuentas.Commands;
using AhorroLand.Application.Features.Cuentas.Queries;
using AhorroLand.Application.Features.Cuentas.Queries.Recent;
using AhorroLand.NuevaApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AhorroLand.NuevaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/cuentas")]
public class CuentasController : AbsController
{
    public CuentasController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetCuentasPagedListQuery(page, pageSize);
        var result = await _sender.Send(query);
        return HandleResult(result); // ??
    }

    /// <summary>
    /// ?? NUEVO: Búsqueda rápida para autocomplete (selectores asíncronos).
    /// </summary>
    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string search, [FromQuery] int limit = 10)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
    ?? User.FindFirst("sub")?.Value
            ?? User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var usuarioId))
        {
            return Unauthorized(new { message = "Usuario no autenticado o token inválido" });
        }

        var query = new SearchCuentasQuery(search, limit)
        {
     UsuarioId = usuarioId
        };

        var result = await _sender.Send(query);
   return HandleResult(result); // ??
    }

    /// <summary>
    /// ?? NUEVO: Obtiene las cuentas más recientes del usuario.
    /// </summary>
    [Authorize]
    [HttpGet("recent")]
    public async Task<IActionResult> GetRecent([FromQuery] int limit = 5)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
     ?? User.FindFirst("sub")?.Value
      ?? User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var usuarioId))
        {
            return Unauthorized(new { message = "Usuario no autenticado o token inválido" });
        }

        var query = new GetRecentCuentasQuery(limit)
        {
     UsuarioId = usuarioId
        };

        var result = await _sender.Send(query);
  return HandleResult(result); // ??
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetCuentaByIdQuery(id);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [Authorize]
[HttpPost]
 public async Task<IActionResult> Create([FromBody] CreateCuentaRequest request)
    {
        var command = new CreateCuentaCommand
        {
     Nombre = request.Nombre,
            Saldo = request.Saldo,
     UsuarioId = request.UsuarioId
        };

        var result = await _sender.Send(command);

      return HandleResultForCreation(
      result,
         nameof(GetById),
        new { id = result.Value }
      );
  }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCuentaRequest request)
    {
        var command = new UpdateCuentaCommand
        {
  Id = id,
          Nombre = request.Nombre
        };

        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
var command = new DeleteCuentaCommand(id);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}

public record CreateCuentaRequest(
    string Nombre,
    decimal Saldo,
    Guid UsuarioId
);

public record UpdateCuentaRequest(
    string Nombre
);
