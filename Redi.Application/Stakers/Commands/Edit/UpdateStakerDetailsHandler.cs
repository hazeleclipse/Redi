using MediatR;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.StakerAggregate;
using Redi.Domain.Common.Enumerations;

namespace Redi.Application.Stakers.Commands.Edit
{
    public class UpdateStakerDetailsHandler : IRequestHandler<UpdateStakerDetails>
    {
        private readonly IStakerRepository _stakerRepository;

        public UpdateStakerDetailsHandler(IStakerRepository stakerRepository)
            => _stakerRepository = stakerRepository;
        
        public async Task<Unit> Handle(UpdateStakerDetails request, CancellationToken cancellationToken)
        {

            await Task.CompletedTask;

            var staker = _stakerRepository.GetById(request.Id)
                ?? throw new Exception("Staker not found");


            if (!Enum.TryParse(request.Role, true, out Role role))
                throw new Exception($"\"{request.Role}\" is not a listed role for the application.");

            staker.UpdateDetails(
                request.Email,
                request.FirstName,
                request.LastName,
                role);

            _stakerRepository.Update(staker);

            return Unit.Value;
        }
    }
}
