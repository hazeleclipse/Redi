using Redi.Domain.Aggregates.StakerAggregate;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

namespace Redi.Application.Persistence
{
    public interface IStakerRepository
    {
        public void Add(Staker staker);
        public List<Staker> GetAll();
        public List<Staker> GetRange(List<StakerId> ids);
        public Staker? GetByEmail(string email);
        public Staker? GetById(StakerId id);
        public void Update(Staker staker);
        public void DeleteById(StakerId id);
    }
}
