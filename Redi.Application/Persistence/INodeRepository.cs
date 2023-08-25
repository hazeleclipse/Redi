using Redi.Domain.Aggregates.NodeAggregate;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;

namespace Redi.Application.Persistence
{
    public interface INodeRepository
    {
        void Add(Node node);

        List<Node> GetAll();

        void DeleteById(NodeId id);
    }
}
