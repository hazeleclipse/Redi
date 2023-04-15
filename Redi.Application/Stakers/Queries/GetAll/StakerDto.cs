namespace Redi.Application.Stakers.Queries.GetAll
{
    public record StakerDto(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        string Role);
}
