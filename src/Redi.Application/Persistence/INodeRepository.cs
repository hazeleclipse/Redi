using Redi.Domain.Aggregates.NodeAggregate;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;

namespace Redi.Application.Persistence
{
    public interface INodeRepository
    {
        void Add(Node node);

        void DeleteById(NodeId id);
        
        List<Node> GetAll();
        
        Node? GetById(NodeId id);
    }
}
