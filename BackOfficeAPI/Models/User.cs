using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackOfficeAPI.Models
{
    public enum Role
    {
        SuperAdmin = 0,
        Admin = 1,
        Candidat = 2
    };
    public class User : IdentityUser
    {
        public User()
        {
        }

        public User(string? nom, string? prenom, string? email, Role role)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Role = role;
        }

        [Key]
        public int UserId { get; set; }
        public string? Nom { get; set; }
        
        public string? Prenom { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public Role Role { get; set; } = Role.Admin;

        /// <summary>
        /// true si l'utilisateur est Connecté
        /// false si l'utilisateur est Déconnecté
        /// </summary>
        public Boolean? Statut { get; set; } = false;
    }
}
