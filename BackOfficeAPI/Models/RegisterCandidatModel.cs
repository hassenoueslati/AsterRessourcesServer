using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackOfficeAPI.Models
{
    public class RegisterCandidatModel
    {

        [Required(ErrorMessage = "Nom is required")]
        public string? Nom { get; set; }
        [Required(ErrorMessage = "Prenom is required")]
        public string? Prenom { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Telephone is required")]
        public int? Telephone { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public string? Image { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Role { get; set; }
    }
}
