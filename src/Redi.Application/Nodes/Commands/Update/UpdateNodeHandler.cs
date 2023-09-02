using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Nodes.Commands.Update;

public class UpdateNodeHandler : IRequestHandler<UpdateNode>
{
    private readonly INodeRepository _repository;

    public UpdateNodeHandler(INodeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateNode request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var currentNode = _repository.GetById(request.Node.Id)
            ?? throw new KeyNotFoundException($"Node {request.Node.Id} does not exist.");

        return Unit.Value;
    }
}
