using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackOfficeAPI.Models
{
    public enum EtatOffre
    {
        Ouverte = 0,
        Suspendu = 1,
        Fermé = 2
    };
    public class Offre
    {
        [Key]
        public int? OffreId { get; set; }
        
        public string? Title { get; set; }
        
        public string? Description { get; set; }
        
        public string? Adresse { get; set; }
        
        public EtatOffre? Etat { get; set; } = EtatOffre.Ouverte;
        
        public List<Proffesion>? Proffesions { get; set; }

        public virtual Admin? Admin { get; set; }
        
        [ForeignKey("Admin")]
        public int? AdminFK { get; set; }

        [JsonIgnore]
        public virtual List<Condidature>? Condidatures { get; set; }
    }
}
