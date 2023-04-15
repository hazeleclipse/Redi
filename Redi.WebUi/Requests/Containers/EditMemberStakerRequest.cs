namespace Redi.WebUi.Requests.Containers
{
    public record EditMemberStakerRequest(Guid ContainerId, Guid StakerId, ushort Weight);
}
