namespace Redi.WebUi.Requests.Authentication
{
    public record LoginRequest(
        string Email,
        string Password);
}
