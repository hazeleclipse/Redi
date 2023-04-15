using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redi.Application.Stakers.Queries.GetDetailsById;
using Redi.WebUi.Exceptions;

namespace Redi.WebUi.Pages.Stakers
{
    [Authorize(Policy = "ResourceOwnerOrAdmin")]
    public class DetailsModel : PageModel
    {
        private readonly ISender _mediator;

        public DetailsModel(ISender mediator)
            => _mediator = mediator;

        public StakerDetailsDto StakerDetails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id is null)
                throw new MissingResourceIdException();

            StakerDetails = await _mediator.Send(new GetStakerDetailsById(id.Value));

            return Page();
        }
    }
}
