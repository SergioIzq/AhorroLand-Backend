using Kash.Shared.Application.Abstractions.Messaging;
using Kash.Shared.Application.Dtos;
namespace Kash.Application.Features.Auth.Queries;


public record GetUserProfileQuery(Guid UserId) : IQuery<UsuarioDto>;
