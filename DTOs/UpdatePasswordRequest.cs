﻿using System.ComponentModel.DataAnnotations;

namespace PresenterAPI.DTOs
{
    public class UpdatePasswordRequest
    {
        [Required(ErrorMessage = "Current password is required.")]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm new password is required.")]
        [Compare("NewPassword", ErrorMessage = "New passwords do not match")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
