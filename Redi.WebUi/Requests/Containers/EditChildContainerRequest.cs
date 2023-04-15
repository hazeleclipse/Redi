namespace Redi.WebUi.Requests.Containers
{
    public record EditChildContainerRequest(Guid ParentId, Guid ChildId, ushort NewWeight);
}
