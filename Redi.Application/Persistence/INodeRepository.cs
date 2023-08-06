using Redi.Domain.Aggregates.NodeAggregate;

namespace Redi.Application.Persistence
{
    public interface INodeRepository
    {
        void Add(Node node);

        List<Node> GetAll();
    }
}
