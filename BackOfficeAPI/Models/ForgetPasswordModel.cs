using System.ComponentModel.DataAnnotations;

namespace BackOfficeAPI.Models
{
    public class ForgetPasswordModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
