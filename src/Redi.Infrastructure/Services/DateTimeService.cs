using Redi.Application.Common.Interfaces.Services;

namespace Redi.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
