namespace Kash.Shared.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime currentTime { get; }
}
