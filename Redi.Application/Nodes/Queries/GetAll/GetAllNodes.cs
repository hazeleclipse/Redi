using MediatR;

namespace Redi.Application.Nodes.Queries.GetAll
{
    public record GetAllNodes() : IRequest<List<NodeDto>>;
}
