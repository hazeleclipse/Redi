using Redi.Domain.Common.Models;
namespace Redi.Domain.Aggregates.ContainerAggregate.ValueObjects;

public class StakerMembershipId : ValueObject
{
    public StakerMembershipId() { }

    public Guid Value { get; }

    private StakerMembershipId(Guid id)
        => Value = id;

    public static StakerMembershipId Create(Guid id)
        => new(id);

    public static implicit operator StakerMembershipId(Guid id)
        => new(id);

    public static implicit operator Guid(StakerMembershipId id)
        => id.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}