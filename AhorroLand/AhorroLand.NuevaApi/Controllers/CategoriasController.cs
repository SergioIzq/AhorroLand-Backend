using AhorroLand.Application.Features.Categorias.Commands;
using AhorroLand.Application.Features.Categorias.Queries;
using AhorroLand.Application.Features.Categorias.Queries.Recent;
using AhorroLand.NuevaApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AhorroLand.NuevaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/categorias")]
public class CategoriasController : AbsController
{
    public CategoriasController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetCategoriasPagedListQuery(page, pageSize);
        var result = await _sender.Send(query);
        return HandlePagedResult(result); // 🆕 Usando HandlePagedResult
    }

    /// <summary>
    /// 🚀 NUEVO: Búsqueda rápida para autocomplete (selectores asíncronos).
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

        var query = new SearchCategoriasQuery(search, limit)
        {
            UsuarioId = usuarioId
        };

        var result = await _sender.Send(query);
        return HandleListResult(result); // 🆕 Usando HandleListResult
    }

    /// <summary>
    /// 🚀 NUEVO: Obtiene las categorías más recientes del usuario.
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

        var query = new GetRecentCategoriasQuery(limit)
        {
            UsuarioId = usuarioId
        };

        var result = await _sender.Send(query);
        return HandleListResult(result); // 🆕 Usando HandleListResult
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetCategoriaByIdQuery(id);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoriaRequest request)
    {
        var command = new CreateCategoriaCommand
        {
            Nombre = request.Nombre,
            UsuarioId = request.UsuarioId,
            Descripcion = request.Descripcion
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
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoriaRequest request)
    {
        var command = new UpdateCategoriaCommand
        {
            Id = id,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion
        };

        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteCategoriaCommand(id);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}

public record CreateCategoriaRequest(
    string Nombre,
    Guid UsuarioId,
    string? Descripcion
);

public record UpdateCategoriaRequest(
    string Nombre,
    string? Descripcion
);
