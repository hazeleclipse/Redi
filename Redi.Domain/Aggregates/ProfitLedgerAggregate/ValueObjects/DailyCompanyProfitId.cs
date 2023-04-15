using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.ProfitLedgerAggregate.ValueObjects;

public class DailyCompanyProfitId : ValueObject
{
    public DailyCompanyProfitId() { }
    public Guid Value { get; }

    private DailyCompanyProfitId(Guid id)
        => Value = id;

    public static DailyCompanyProfitId Create(Guid id)
        => new(id);

    public static implicit operator DailyCompanyProfitId(Guid id)
        => new(id);

    public static implicit operator Guid(DailyCompanyProfitId id)
        => id.Value;

    public static implicit operator Guid?(DailyCompanyProfitId? id)
    => id?.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
