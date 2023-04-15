using MediatR;
using Redi.Application.Common.Interfaces.Authentication;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.StakerAggregate;
using Redi.Domain.Common.Enumerations;

namespace Redi.Application.Stakers.Commands.Register
{
    public class RegisterStakerHandler : IRequestHandler<RegisterStaker>
    {
        private readonly IStakerRepository _stakerRepository;
        private readonly IRediPasswordHasher _passwordHasher;

        public RegisterStakerHandler(IStakerRepository stakerRepository, IRediPasswordHasher passwordHasher)
        {
            _stakerRepository = stakerRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(RegisterStaker request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (_stakerRepository.GetByEmail(request.Email) is not null)
                throw new Exception("Staker with email already exists");

            if (!Enum.TryParse(request.Role, true, out Role role))
                throw new Exception($"\"{request.Role}\" is not a listed role for the application.");

            var newStaker = Staker.Create(
                Guid.NewGuid(),
                request.Email,
                request.FirstName,
                request.LastName,
                _passwordHasher.HashPassword(request.Password),
                role);

            _stakerRepository.Add(newStaker);

            return Unit.Value;
        }
    }
}
