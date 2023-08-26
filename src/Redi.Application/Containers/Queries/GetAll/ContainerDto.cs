namespace Redi.Application.Containers.Queries.GetAll
{
    public record ContainerDto(
        Guid Id,
        string Name,
        Guid? ParentId,
        string? ParentName,
        decimal Stake,
        decimal LocalStake);
}
