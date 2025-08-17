using System.ComponentModel.DataAnnotations;

namespace PresenterAPI.DTOs
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm password is required.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
