namespace Redi.WebUi.Requests.Containers
{
    public record CreateContainerRequest(
        string Name,
        Guid ParentId,
        ushort Weight);
}
