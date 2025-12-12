namespace Kash.Shared.Application.Interfaces;

/// <summary>
/// Proporciona información del contexto del usuario actual.
/// </summary>
public interface IUserContext
{
    Guid? UserId { get; }
}
