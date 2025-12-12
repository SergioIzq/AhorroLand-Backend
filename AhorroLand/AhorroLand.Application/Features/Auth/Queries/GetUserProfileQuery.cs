using AhorroLand.Shared.Application.Abstractions.Messaging;
using AhorroLand.Shared.Application.Dtos;
namespace AhorroLand.Application.Features.Auth.Queries;


public record GetUserProfileQuery(Guid UserId) : IQuery<UsuarioDto>;
