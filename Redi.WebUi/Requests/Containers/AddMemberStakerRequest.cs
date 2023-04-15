namespace Redi.WebUi.Requests.Containers
{
    public record AddMemberStakerRequest(Guid ContainerId, Guid StakerId, ushort Weight);
}
