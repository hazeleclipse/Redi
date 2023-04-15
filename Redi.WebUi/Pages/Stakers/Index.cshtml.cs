using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redi.Application.Stakers.Queries.GetAll;

namespace Redi.WebUi.Pages.Stakers
{
    public class IndexModel : PageModel
    {
        private readonly ISender _mediator;

        public IndexModel(ISender mediator)
            => _mediator = mediator;

        public List<StakerDto> Stakers { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Stakers = await _mediator.Send(new GetAllStakers());

            return Page();
        }
    }
}
