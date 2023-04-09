namespace Fitsou.Application.Contracts;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}