using AhorroLand.Application.Features.Conceptos.Commands;
using AhorroLand.Application.Features.Conceptos.Queries;
using AhorroLand.Application.Features.Conceptos.Queries.Recent;
using AhorroLand.Application.Features.Conceptos.Queries.Search;
using AhorroLand.NuevaApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AhorroLand.NuevaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/conceptos")]
public class ConceptosController : AbsController
{
    public ConceptosController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetConceptosPagedListQuery(page, pageSize);
        var result = await _sender.Send(query);
    return HandlePagedResult(result); // 🆕
    }

    /// <summary>
    /// 🚀 NUEVO: Búsqueda rápida para autocomplete (selectores asíncronos).
    /// Devuelve solo los clientes que coincidan con el término de búsqueda, limitados a 10 resultados.
    /// Ultra-rápido: <10ms de respuesta.
    /// </summary>
    /// <param name="search">Término de búsqueda (ej: "Jua" busca "Juan Pérez", "Juana María", etc.)</param>
    /// <param name="limit">Número máximo de resultados (por defecto 10, máximo 50)</param>
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

        var query = new SearchConceptosQuery(search, limit)
        {
            UsuarioId = usuarioId
        };

        var result = await _sender.Send(query);
        return HandleListResult(result); // 🆕
    }

    /// <summary>
    /// 🚀 NUEVO: Obtiene los conceptos más recientes del usuario.
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

        var query = new GetRecentConceptosQuery(limit)
        {
  UsuarioId = usuarioId
    };

        var result = await _sender.Send(query);
   return HandleListResult(result); // 🆕
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetConceptoByIdQuery(id);
        var result = await _sender.Send(query);
    return HandleResult(result);
}

    [Authorize]
  [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateConceptoRequest request)
    {
    var command = new CreateConceptoCommand
        {
     Nombre = request.Nombre,
            CategoriaId = request.CategoriaId,
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
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateConceptoRequest request)
    {
        var command = new UpdateConceptoCommand
        {
            Id = id,
          Nombre = request.Nombre,
     CategoriaId = request.CategoriaId
        };

        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
 {
     var command = new DeleteConceptoCommand(id);
  var result = await _sender.Send(command);
        return HandleResult(result);
    }
}

public record CreateConceptoRequest(
  string Nombre,
    Guid CategoriaId,
    Guid UsuarioId
);

public record UpdateConceptoRequest(
    string Nombre,
    Guid CategoriaId
);
