namespace Redi.MinimalApi.Dto
{
    public record CreateStakerRequest(
        string Email,
        string FirstName,
        string LastName,
        string Password,
        string Role);
}
