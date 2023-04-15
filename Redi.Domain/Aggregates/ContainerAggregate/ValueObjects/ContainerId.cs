using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.ContainerAggregate.ValueObjects;

public class ContainerId : ValueObject
{
    public ContainerId() { }

    public Guid Value { get; }

    private ContainerId(Guid id)
        => Value = id;

    public static ContainerId Create(Guid id)
        => new(id);

    public static implicit operator ContainerId(Guid id)
        => new(id);        

    public static implicit operator Guid(ContainerId id)
        => id.Value;

    public static implicit operator Guid?(ContainerId? id)
    => id?.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
