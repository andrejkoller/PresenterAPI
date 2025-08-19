namespace PresenterAPI.DTOs
{
    public class AuthResponse
    {
        public PublicUser User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
    }
}
