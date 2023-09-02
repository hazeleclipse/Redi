using MediatR;

namespace Redi.Application.Nodes.Queries.GetById;

public record GetNodeById(Guid Id) : IRequest<NodeDto?>;

