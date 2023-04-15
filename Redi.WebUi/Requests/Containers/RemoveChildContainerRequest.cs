namespace Redi.WebUi.Requests.Containers
{
    public record RemoveChildContainerRequest(Guid ParentId, Guid ChildId);
}
