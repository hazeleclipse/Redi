using MediatR;

namespace Redi.Application.Nodes.Commands.Create
{
    public record CreateNode(string Name, string NodeType) : IRequest<NodeDto>;
}
