namespace Redi.WebUi.Requests.Containers
{
    public record AddChildContainerRequest(Guid ParentId, Guid ChildId, ushort Weight);
}
