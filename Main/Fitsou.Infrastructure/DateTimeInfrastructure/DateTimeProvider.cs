using Fitsou.Application.Contracts;

namespace Infrastructure.DateTimeInfrastructure;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}