namespace Redi.WebUi.Requests.Stakers
{
    public record RegisterRequest(
        string Email,
        string FirstName,
        string LastName,
        string Password,
        string Role);
}
