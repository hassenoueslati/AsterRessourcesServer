using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int OffreId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        public EtatOffre Etat { get; set; } = EtatOffre.Ouverte;
        [Required]
        public List<Proffesion> Proffesions { get; set; }

        public virtual Admin Admin { get; set; }
        [ForeignKey("Admin")]
        public int AdminFK { get; set; }
        public virtual List<Condidature>? Condidatures { get; set; }
    }
}
