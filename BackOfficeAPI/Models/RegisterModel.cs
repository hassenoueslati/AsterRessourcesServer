using System.ComponentModel.DataAnnotations;

namespace BackOfficeAPI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nom is required")]
        public string? Nom { get; set; }
        [Required(ErrorMessage = "Prenom is required")]
        public string? Prenom { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
