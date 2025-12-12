using AhorroLand.Shared.Domain.ValueObjects;

namespace AhorroLand.Domain;

public interface IUsuarioReadRepository
{
    Task<Usuario?> GetByEmailAsync(Email correo, CancellationToken cancellationToken = default);
    Task<Usuario?> GetByConfirmationTokenAsync(string token, CancellationToken cancellationToken = default);
}