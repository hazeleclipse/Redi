using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

public class StakerId : ValueObject
{
    public Guid Value { get; }

    public StakerId() { }

    private StakerId(Guid id)
        => Value = id;

    public static StakerId Create(Guid id)
        => new(id);

    public static StakerId CreateUnique() 
        => new(Guid.NewGuid());

    public static implicit operator StakerId(Guid id)
        => new(id);        

    public static implicit operator Guid(StakerId id)
        => id.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
