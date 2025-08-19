using System.ComponentModel.DataAnnotations;

namespace PresenterAPI.DTOs
{
    public class UpdateEmailRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
    }
}
