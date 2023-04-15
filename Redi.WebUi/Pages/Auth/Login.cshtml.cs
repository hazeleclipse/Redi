using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redi.Application.Authentication.Queries.Login;
using Redi.Application.Common.Interfaces.Authentication;
using Redi.WebUi.Requests.Authentication;

namespace Redi.WebUi.Pages.Auth
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ISender _mediator;
        private readonly IClaimsPrincipalGenerator _claimsPrincipalGenerator;

        public LoginModel(ISender mediator, IClaimsPrincipalGenerator claimsPrincipalGenerator)
        {
            _mediator = mediator;
            _claimsPrincipalGenerator = claimsPrincipalGenerator;
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = default!;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (LoginRequest is null)
                return BadRequest(ModelState);

            // Preform Authentication
            var loginQuery = new LoginQuery(LoginRequest.Email, LoginRequest.Password);
            var authenticatedUser = await _mediator.Send(loginQuery);

            if (authenticatedUser is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // Create cookie and add to response
            await HttpContext.SignInAsync(
                "access-token",
                _claimsPrincipalGenerator.Generate(authenticatedUser));

            // Send user to their details page
            if (authenticatedUser.Role == "Admin")
                return RedirectToPage("/Index");
            else
                return RedirectToPage("/Stakers/Details", new {id = authenticatedUser.Id});
        }
    }
}
