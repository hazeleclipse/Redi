using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redi.Application.Containers.Commands.AddChild;
using Redi.Application.Containers.Commands.Edit;
using Redi.Application.Containers.Commands.EditChild;
using Redi.Application.Containers.Commands.RemoveChild;
using Redi.Application.Containers.Queries.GetDetailsById;
using Redi.WebUi.Exceptions;
using Redi.WebUi.Requests.Containers;

namespace Redi.WebUi.Pages.Containers
{
    public class DetailsModel : PageModel
    {
        private readonly ISender _mediator;

        public DetailsModel(ISender mediator)
        {
            _mediator = mediator;
        }

        public ContainerDetailsDto ContainerDetails { get; set; } = default!;

        [BindProperty]
        public EditContainerRequest EditContainerRequest { get; set; } = default!;

        [BindProperty]
        public EditChildContainerRequest EditChildContainerRequest { get; set; } = default!;

        [BindProperty]
        public EditMemberStakerRequest EditChildStakerRequest { get; set; } = default!;

        [BindProperty]
        public AddChildContainerRequest AddChildContainerRequest { get; set; } = default!;

        [BindProperty]
        public AddMemberStakerRequest AddChildStakerRequest { get; set; } = default!;

        [BindProperty]
        public RemoveChildContainerRequest RemoveChildContainerRequest { get; set; } = default!;

        [BindProperty]
        public RemoveMemberStakerRequest RemoveChildStakerRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id is null)
                throw new MissingResourceIdException();

            ContainerDetails = await _mediator.Send(new GetContainerDetailsById(id.Value));

            return Page();
        }

        public async Task<IActionResult> OnGetEditAsync(Guid? id)
        {
            if (id is null)
                throw new MissingResourceIdException();

            ContainerDetails = await _mediator.Send(new GetContainerDetailsById(id.Value));

            EditContainerRequest = new EditContainerRequest(id.Value, ContainerDetails.Name);

            return Page();
        }

        public async Task<IActionResult> OnGetEditChildAsync(Guid? id, Guid? childId)
        {
            if (id is null)
                throw new MissingResourceIdException();
            if (childId is null)
                throw new Exception("Please provide the childId to edit as a query parameter");

            ContainerDetails = await _mediator.Send(new GetContainerDetailsById(id.Value));

            var child = ContainerDetails.ChildContainers.Where(cc => cc.Id == childId).First()
                ?? throw new Exception($"Container {childId} is not a child of container {id}.");

            EditChildContainerRequest = new EditChildContainerRequest(id.Value, child.Id, child.Weight);

            return Page();
        }

        public async Task<IActionResult> OnGetEditChildStakerAsync(Guid? id, Guid? stakerId)
        {
            if (id is null)
                throw new MissingResourceIdException();
            if (stakerId is null)
                throw new Exception("Please provide the stakerId to edit as a query parameter");

            ContainerDetails = await _mediator.Send(new GetContainerDetailsById(id.Value));

            var member = ContainerDetails.Memberships.Where(sm => sm.Id == stakerId).First()
                ?? throw new Exception($"Staker {stakerId} is not a member of container {id}.");

            EditChildStakerRequest = new EditMemberStakerRequest(id.Value, member.Id, member.Weight);

            return Page();
        }

        public async Task<IActionResult> OnPostAddChildAsync(Guid id)
        {
            if (AddChildContainerRequest is null)
                throw new Exception("Add child container request is not valid");

            var addChild = new AddChildContainer(id, AddChildContainerRequest.ChildId);
            await _mediator.Send(addChild);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddChildStakerAsync(Guid id)
        {
            if (AddChildStakerRequest is null)
                throw new Exception("Add child staker request is not valid");

            var addStaker = new AddChildStaker(id, AddChildStakerRequest.StakerId);
            await _mediator.Send(addStaker);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveChildAsync(Guid id)
        {

            if (RemoveChildContainerRequest is null)
                throw new Exception("Add child container request is not valid");

            var removeChild = new RemoveChildContainer(id, RemoveChildContainerRequest.ChildId);
            await _mediator.Send(removeChild);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveChildStakerAsync(Guid id)
        {

            if (RemoveChildStakerRequest is null)
                throw new Exception("Add child container request is not valid");

            var removeStaker = new RemoveChildStaker(id, RemoveChildStakerRequest.StakerId);
            await _mediator.Send(removeStaker);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSaveAsync(Guid id)
        {
            if (EditContainerRequest is null)
                throw new Exception("Missing new container name");

            var edit = new EditContainer(id, EditContainerRequest.NewName);
            await _mediator.Send(edit);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSaveChildAsync(Guid id)
        {
            if (EditChildContainerRequest is null)
                throw new Exception("Edit child container request is invalid");

            var editChildWeight = new EditChildContainerWeight(id,
                EditChildContainerRequest.ChildId, EditChildContainerRequest.NewWeight);
            await _mediator.Send(editChildWeight);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSaveChildStakerAsync(Guid id)
        {
            if (EditChildStakerRequest is null)
                throw new Exception("Edit child staker request is invalid");

            var editChildWeight = new EditChildStakerWeight(id,
                EditChildStakerRequest.StakerId, EditChildStakerRequest.Weight);
            await _mediator.Send(editChildWeight);

            return RedirectToPage();
        }
    }
}
