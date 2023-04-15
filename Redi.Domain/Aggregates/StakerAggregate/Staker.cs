using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.Enumerations;
using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.StakerAggregate;

public class Staker : AggregateRoot<StakerId>
{
    public EmailAddress Email { get; private set; } = default!;
    public FirstName FirstName { get; private set; } = default!;
    public LastName LastName { get; private set; } = default!;
    public Password Password { get; private set; } = default!;
    public Role Role { get; private set; } = default!;

    public Staker() { }

    private Staker(StakerId id,
        EmailAddress emailAddress,
        FirstName firstName,
        LastName lastName,
        Password password,
        Role role) : base(id)
    {
        Email = emailAddress;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Role = role;
    }

    public static Staker Create(StakerId id,
        EmailAddress emailAddress,
        FirstName firstName,
        LastName lastName,
        Password password,
        Role role)
    {
        return new Staker(
            id,
            emailAddress,
            firstName,
            lastName,
            password,
            role);
    }

    public void UpdateDetails(EmailAddress email, FirstName firstName, LastName lastName, Role role)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
    }
}
