using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.ProfitLedgerAggregate.ValueObjects;

public class DailyStakerProfitId : ValueObject
{
    public DailyStakerProfitId() { }
    public Guid Value { get; }

    private DailyStakerProfitId(Guid id)
        => Value = id;

    public static DailyStakerProfitId Create(Guid id)
        => new(id);

    public static implicit operator DailyStakerProfitId(Guid id)
        => new(id);

    public static implicit operator Guid(DailyStakerProfitId id)
        => id.Value;

    public static implicit operator Guid?(DailyStakerProfitId? id)
    => id?.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
