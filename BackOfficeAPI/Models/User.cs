using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackOfficeAPI.Models
{
    public enum Role
    {
        SuperAdmin = 0,
        Admin = 1,
        Condidat = 2
    };
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string? Nom { get; set; }
        
        public string? Prenom { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public Role Role { get; set; } = Role.Admin;

    }
}
