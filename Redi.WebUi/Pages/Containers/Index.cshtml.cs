using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redi.Application.Containers.Commands.Create;
using Redi.Application.Containers.Commands.Delete;
using Redi.Application.Containers.Queries.GetAll;
using Redi.WebUi.Requests.Containers;

namespace Redi.WebUi.Pages.Containers
{
    public class IndexModel : PageModel
    {
        private readonly ISender _mediator;

        public IndexModel(ISender mediator)
            => _mediator = mediator;
        
        public List<ContainerDto> Containers = new();

        [BindProperty]
        public CreateContainerRequest CreateContainerRequest { get; set; } = default!;

        [BindProperty]
        public DeleteContainerRequest DeleteContainerRequest { get; set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            Containers = await _mediator.Send(new GetAllContainers());

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (DeleteContainerRequest is null)
                throw new Exception("Misssing Id of container to delete");

            await _mediator.Send(new DeleteContainer(DeleteContainerRequest.Id));

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (CreateContainerRequest is null)
                throw new Exception("Create container request is invalid");

            await _mediator.Send(new CreateContainer(CreateContainerRequest.Name));

            return RedirectToPage();
        }
    }
}
