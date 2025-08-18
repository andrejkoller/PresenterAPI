namespace PresenterAPI.DTOs
{
    public class LoginResponse
    {
        public PublicUser User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
    }
}
