namespace SmartMenu.Services.AuthAPI.Models.Dto
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } // The user's current password
        public string NewPassword { get; set; } // The new password
        public string ConfirmNewPassword { get; set; } // Confirm the new password
    }
}
