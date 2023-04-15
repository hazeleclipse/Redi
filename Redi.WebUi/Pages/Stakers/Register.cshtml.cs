using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redi.Application.Stakers.Commands.Register;
using Redi.WebUi.Requests.Stakers;

namespace Redi.WebUi.Pages.Stakers
{
    public class RegisterModel : PageModel
    {
        private readonly ISender _mediator;

        public RegisterModel(ISender mediator)
            => _mediator = mediator;        

        [BindProperty]
        public RegisterRequest RegisterRequest { get; set; } = default!;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (RegisterRequest is null)
                return BadRequest(ModelState);

            var register = new RegisterStaker(
                RegisterRequest.Email,
                RegisterRequest.FirstName,
                RegisterRequest.LastName,
                RegisterRequest.Password,
                RegisterRequest.Role);

            _mediator.Send(register);

            return RedirectToPage("./Index");
        }
    }
}
