namespace Redi.Application.Containers.Queries.GetDetailsById
{
    public record ContainerDetailsDto(
        Guid Id,
        string Name,
        Guid? ParentId,
        string? ParentName,
        decimal Stake,
        decimal LocalStake,
        IEnumerable<ChildContainerDto> ChildContainers,
        IEnumerable<MemberStakerDto> Memberships);

    public record MemberStakerDto(
        Guid Id,
        string FirstName,
        string LastName,
        decimal Stake,
        decimal LocalStake,
        ushort Weight);

    public record ChildContainerDto(
        Guid Id,
        string Name,
        decimal Stake,
        decimal LocalStake,
        ushort Weight);
}
