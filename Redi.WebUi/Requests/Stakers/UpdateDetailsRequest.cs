namespace Redi.WebUi.Requests.Stakers
{
    public record UpdateDetailsRequest(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        string Role);
}
