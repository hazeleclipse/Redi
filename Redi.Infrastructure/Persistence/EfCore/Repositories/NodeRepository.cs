using Redi.Application.Persistence;
using Redi.Domain.Aggregates.NodeAggregate;

namespace Redi.Infrastructure.Persistence.EfCore.Repositories
{
    internal class NodeRepository : INodeRepository
    {
        private readonly RediDbContext _rediDbContext;

        public NodeRepository(RediDbContext rediDbContext)
            => _rediDbContext = rediDbContext;

        public void Add(Node container)
        {
            if (_rediDbContext.Nodes.Contains(container))
                return;

            _rediDbContext.Nodes.Add(container);

            _rediDbContext.SaveChanges();
        }
    }
}
