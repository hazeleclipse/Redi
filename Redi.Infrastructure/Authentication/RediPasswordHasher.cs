using Redi.Application.Common.Interfaces.Authentication;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

namespace Redi.Infrastructure.Authentication
{
    public class RediPasswordHasher : IRediPasswordHasher
    {
        public string HashPassword( Password password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyHashedPassword( Password storedPassword, Password suppliedPassword)
        {
            var result = BCrypt.Net.BCrypt.Verify(suppliedPassword, storedPassword);
            return result;
        }
    }
}
