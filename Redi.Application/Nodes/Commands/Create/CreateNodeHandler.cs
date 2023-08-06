using MediatR;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.NodeAggregate;

namespace Redi.Application.Nodes.Commands.Create
{
    internal class CreateNodeHandler : IRequestHandler<CreateNode, NodeDto>
    {
        private readonly INodeRepository _nodeRepository;

        public CreateNodeHandler(INodeRepository nodeRepository)
            => _nodeRepository = nodeRepository;

        public async Task<NodeDto> Handle(CreateNode request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            Node? newNode = default;

            // Create new node
            switch (request.NodeType)
            {
                case "CoreNode":
                    newNode = CoreNode.Create(
                        Guid.NewGuid(),
                        request.Name,
                        null);
                    break;
                case "ByWeightNode":
                    newNode = ByWeightNode.Create(
                        Guid.NewGuid(),
                        request.Name,
                        null);
                    break;
                default:
                    throw new ArgumentException("NodeType was not a valid value");
            }


            // Persist
            _nodeRepository.Add(newNode);

            return new(newNode.Id, newNode.Name, newNode.GetType().Name);
        }
    }
}
