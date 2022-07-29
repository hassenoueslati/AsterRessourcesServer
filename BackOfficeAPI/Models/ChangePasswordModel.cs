using System.ComponentModel.DataAnnotations;

namespace BackOfficeAPI.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "CurrentPassword is required")]
        public string? CurrentPassword { get; set; }
        [Required(ErrorMessage = "NewPassword is required")]
        public string? NewPassword { get; set; }
        [Required(ErrorMessage = "ConfirmNewPassword is required")]
        public string? ConfirmNewPassword { get; set; }
    }
}
