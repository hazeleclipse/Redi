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

        var node = _repository.GetById(request.Id)
            ?? throw new KeyNotFoundException($"Node {request.Id} does not exist.");

        node.Update(request.Name);

        _repository.Update(node);

        return Unit.Value;
    }
}
