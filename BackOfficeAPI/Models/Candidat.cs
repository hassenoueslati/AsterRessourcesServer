using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackOfficeAPI.Models
{
    public enum Proffesion
    {
        Menuisier = 0,
        ChefCuisine = 1,
        Machiniste = 2,
        JournalierDeProduction = 3,
        CommisEntrepôt = 4,
        EtalagisteDécorateur = 5
    }
    public class Candidat : User
    {
        
        public int? Telephone { get; set; }
        public string? Image { get; set; }
        
        public List<Proffesion>? Proffesions { get; set; }
        [JsonIgnore]
        public virtual List<Condidature>? Condidatures { get; set; }
        

    }
}
