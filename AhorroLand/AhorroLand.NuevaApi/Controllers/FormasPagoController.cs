using AhorroLand.Application.Features.FormasPago.Commands;
using AhorroLand.Application.Features.FormasPago.Queries;
using AhorroLand.Application.Features.FormasPago.Queries.Recent;
using AhorroLand.Application.Features.FormasPago.Queries.Search;
using AhorroLand.NuevaApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AhorroLand.NuevaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/formas-pago")]
public class FormasPagoController : AbsController
{
    public FormasPagoController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetFormasPagoPagedListQuery(page, pageSize);
        var result = await _sender.Send(query);
   return HandlePagedResult(result); // ??
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

        var query = new SearchFormasPagoQuery(search, limit)
    {
 UsuarioId = usuarioId
        };

  var result = await _sender.Send(query);
        return HandleListResult(result); // ??
    }

    /// <summary>
    /// ?? NUEVO: Obtiene las formas de pago más recientes del usuario.
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

      var query = new GetRecentFormasPagoQuery(limit)
        {
 UsuarioId = usuarioId
        };

  var result = await _sender.Send(query);
        return HandleListResult(result); // ??
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetFormaPagoByIdQuery(id);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFormaPagoRequest request)
    {
 var command = new CreateFormaPagoCommand
        {
     Nombre = request.Nombre,
       UsuarioId = request.UsuarioId
        };

 var result = await _sender.Send(command);

    return HandleResultForCreation(
  result,
 nameof(GetById),
    new { id = result.Value.Id }
        );
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFormaPagoRequest request)
    {
 var command = new UpdateFormaPagoCommand
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
        var command = new DeleteFormaPagoCommand(id);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}

public record CreateFormaPagoRequest(
    string Nombre,
    Guid UsuarioId
);

public record UpdateFormaPagoRequest(
    string Nombre
);
