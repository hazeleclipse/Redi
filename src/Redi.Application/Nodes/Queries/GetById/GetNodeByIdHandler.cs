using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Nodes.Queries.GetById
{
    internal class GetNodeByIdHandler : IRequestHandler<GetNodeById, NodeDto?>
    {
        private readonly INodeRepository _nodeRepository;

        public GetNodeByIdHandler(INodeRepository nodeRepository)
        {
            _nodeRepository = nodeRepository ?? throw new ArgumentNullException(nameof(_nodeRepository));
        }


        public async Task<NodeDto?> Handle(GetNodeById request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var node = _nodeRepository.GetById(request.Id);

            if (node is null) return null;

            return new NodeDto(node.Id, node.Name, node.GetType().Name);
        }
    }
}
