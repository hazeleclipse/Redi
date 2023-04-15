namespace Redi.Application.Stakers.Queries.GetById
{
    public record StakerDto(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        string Role);
}
