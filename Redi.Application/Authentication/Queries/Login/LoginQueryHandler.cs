using MediatR;
using Redi.Application.Authentication.Queries.Login;
using Redi.Application.Common.Interfaces.Authentication;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.StakerAggregate;
using System.Security.Authentication;

namespace Redi.Application.Authentication.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IStakerRepository _stakerRepository;
        private readonly IRediPasswordHasher _passwordHasher;

        public LoginQueryHandler(IStakerRepository stakerRepository, IRediPasswordHasher passwordHasher)
        {
            _stakerRepository = stakerRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<AuthenticationResult> Handle(
            LoginQuery request,
            CancellationToken cancellationToken)
        {
            // Temp to remove 'await' warning
            await Task.CompletedTask;

            Staker staker = _stakerRepository.GetByEmail(request.Email)
                ?? throw new InvalidCredentialException("Either the email, password, or both are incorrect.");

            // Check password
            if (!_passwordHasher.VerifyHashedPassword(staker.Password, request.Password))
                throw new InvalidCredentialException("Either the email, password, or both are incorrect.");                

            return new AuthenticationResult(
                staker.Id,
                staker.Email,
                staker.FirstName,
                staker.LastName,
                staker.Role.ToString());
        }
    }
}
