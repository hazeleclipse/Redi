using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

namespace Redi.Application.Common.Interfaces.Authentication
{
    public interface IRediPasswordHasher
    {
        string HashPassword(Password password);

        bool VerifyHashedPassword(Password storedPassword, Password suppliedPassword);

    }
}
