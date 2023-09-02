using MediatR;
using Redi.Domain.Aggregates.NodeAggregate;

namespace Redi.Application.Nodes.Commands.Update;

public record UpdateNode(Node Node) : IRequest;
