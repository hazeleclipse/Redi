using MediatR;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;

namespace Redi.Application.Nodes.Commands.Delete;

public record DeleteNode(Guid Id) : IRequest;
