using MediatR;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.StakerAggregate;

namespace Redi.Application.Stakers.Commands.Register
{
    public class RegisterStakerHandler : IRequestHandler<RegisterStaker, StakerDto>
    {
        private readonly IStakerRepository _stakerRepository;

        public RegisterStakerHandler(IStakerRepository stakerRepository)
        {
            _stakerRepository = stakerRepository;
        }

        public async Task<StakerDto> Handle(RegisterStaker request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var newStaker = Staker.Create(Guid.NewGuid());

            _stakerRepository.Add(newStaker);

            return new StakerDto(Id: newStaker.Id);
        }
    }
}
