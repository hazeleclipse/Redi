namespace Redi.MinimalApi.Stakers
{
    public record UpdateStakerRequest(
        string Email,
        string FirstName,
        string LastName, 
        string Password,
        string Role);
}
