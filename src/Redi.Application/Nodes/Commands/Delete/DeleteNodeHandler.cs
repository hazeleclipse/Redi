using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Nodes.Commands.Delete;

public class DeleteNodeHandler : IRequestHandler<DeleteNode>
{
    private readonly INodeRepository _repository;

    public DeleteNodeHandler(INodeRepository repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(DeleteNode request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        _repository.DeleteById(request.Id);

        return Unit.Value;
    }
}
