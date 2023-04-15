using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redi.Application.Stakers.Commands.Edit;
using Redi.Application.Stakers.Queries.GetById;
using Redi.WebUi.Exceptions;
using Redi.WebUi.Requests.Stakers;

namespace Redi.WebUi.Pages.Stakers
{
    public class EditModel : PageModel
    {
        private readonly ISender _mediator;

        public EditModel(ISender mediator)
            => _mediator = mediator;

        [BindProperty]
        public UpdateDetailsRequest UpdateRequest { get; set; } = default!;     

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id is null)
                throw new MissingResourceIdException();

            var getById = new GetStakerById(id.Value);
            var staker = await _mediator.Send(getById);

            UpdateRequest = new UpdateDetailsRequest(
                staker.Id,
                staker.Email,
                staker.FirstName,
                staker.LastName,
                staker.Role);
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id is null)
                throw new MissingResourceIdException();

            var update = new UpdateStakerDetails(
                id,
                UpdateRequest.Email,
                UpdateRequest.FirstName,
                UpdateRequest.LastName,
                UpdateRequest.Role);

            await _mediator.Send(update);

            return RedirectToPage("./details", new {id});
        }
    }
}
