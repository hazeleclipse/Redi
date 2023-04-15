using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Stakers.Commands.Delete
{
    public class DeleteStakerHandler : IRequestHandler<DeleteStaker>
    {
        private readonly IStakerRepository _stakerRepository;

        public DeleteStakerHandler(IStakerRepository stakerRepository)
            => _stakerRepository = stakerRepository;

        public async Task<Unit> Handle(DeleteStaker request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            _stakerRepository.DeleteById(request.Id);

            return Unit.Value;
        }
    }
}
