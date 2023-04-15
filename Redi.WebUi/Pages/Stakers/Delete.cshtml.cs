using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redi.Application.Stakers.Commands.Delete;
using Redi.Application.Stakers.Queries.GetById;
using Redi.WebUi.Exceptions;


namespace Redi.WebUi.Pages.Stakers
{
    public class DeleteModel : PageModel
    {
        private readonly ISender _mediator;

        public DeleteModel(ISender mediator)
            => _mediator = mediator;

        public StakerDto Staker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id is null)
                throw new MissingResourceIdException();

            Staker = await _mediator.Send(new GetStakerById(id.Value));

            return Page();
        }

        public IActionResult OnPost(Guid? id) 
        {
            if (id is null)
                throw new MissingResourceIdException();

            _mediator.Send(new DeleteStaker(id.Value));

            return RedirectToPage("./Index");
        }
    }
}
