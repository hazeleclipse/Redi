namespace Redi.Application.Stakers.Commands.Register
{
    public record StakerDto(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        string Role);
}
