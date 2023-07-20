using Redi.Application.Persistence;
using Redi.Domain.Aggregates.StakerAggregate;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Repositories
{
    public class StakerRepository : IStakerRepository
    {
        
        private readonly RediDbContext _rediDbContext;

        public StakerRepository(RediDbContext rediDbContext)
            => _rediDbContext = rediDbContext;
        

        public void Add(Staker staker)
        {
            _rediDbContext.Stakers.Add(staker);
            _rediDbContext.SaveChanges();
        }

        public void DeleteById(StakerId id)
        {
            var staker = _rediDbContext.Stakers.Find(id)
                ?? throw new Exception("Staker not found");

            _rediDbContext.Stakers.Remove(staker);

            _rediDbContext.SaveChanges();
        }

        public List<Staker> GetAll()
        {
            return _rediDbContext.Stakers.ToList();
        }

        public Staker? GetById(StakerId id)
        {
            return _rediDbContext.Stakers.Find(id);
        }

        public List<Staker> GetRange(List<StakerId> ids)
        {
            var returnList = new List<Staker>();
            foreach (var id in ids)
            {
                var staker = GetById(id);
                if (staker is not null)
                    returnList.Add(staker);
            }
            return returnList;
        }
    }
}
