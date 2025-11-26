using AhorroLand.Application.Features.Categorias.Queries;
using AhorroLand.Application.Features.Personas.Commands;
using AhorroLand.Application.Features.Personas.Queries;
using AhorroLand.Application.Features.Personas.Queries.Recent;
using AhorroLand.NuevaApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AhorroLand.NuevaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/personas")]
public class PersonasController : AbsController
{
    public PersonasController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetPersonasPagedListQuery(page, pageSize);
        var result = await _sender.Send(query);
        return HandlePagedResult(result); // 🆕
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

        var query = new SearchPersonasQuery(search, limit)
        {
            UsuarioId = usuarioId
        };

        var result = await _sender.Send(query);
        return HandleListResult(result); // 🆕
    }

    /// <summary>
    /// 🚀 NUEVO: Obtiene las personas más recientes del usuario.
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

        var query = new GetRecentPersonasQuery(limit)
        {
            UsuarioId = usuarioId
        };

        var result = await _sender.Send(query);
        return HandleListResult(result); // 🆕
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetPersonaByIdQuery(id);
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonaRequest request)
    {
        var command = new CreatePersonaCommand
        {
            Nombre = request.Nombre,
            UsuarioId = request.UsuarioId
        };

        var result = await _sender.Send(command);

        return HandleResultForCreation(
            result,
            nameof(GetById),
            new { id = result.Value }
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonaRequest request)
    {
        var command = new UpdatePersonaCommand
        {
            Id = id,
            Nombre = request.Nombre
        };

        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeletePersonaCommand(id);
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}

public record CreatePersonaRequest(
    string Nombre,
    Guid UsuarioId
);

public record UpdatePersonaRequest(
    string Nombre
);
