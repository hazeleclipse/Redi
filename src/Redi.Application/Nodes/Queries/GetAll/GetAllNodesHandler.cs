using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Nodes.Queries.GetAll
{
    internal class GetAllNodesHandler : IRequestHandler<GetAllNodes, List<NodeDto>>
    {
        private readonly INodeRepository _nodeRepository;

        public GetAllNodesHandler(INodeRepository nodeRepository)
            => _nodeRepository = nodeRepository;

        public async Task<List<NodeDto>> Handle(GetAllNodes request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var nodes = new List<NodeDto>();

            _nodeRepository.GetAll().ForEach(n =>
            {
                nodes.Add(new NodeDto(
                    n.Id,
                    n.Name,
                    n.GetType().Name));
            });

            return nodes;
        }
    }
}
