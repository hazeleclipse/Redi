using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.Enumerations;
using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.StakerAggregate;

public class Staker : AggregateRoot<StakerId>
{


    public Staker() { }

    private Staker(StakerId id) : base(id) { }

    public static Staker Create(StakerId id)
        => new Staker(id);

}
