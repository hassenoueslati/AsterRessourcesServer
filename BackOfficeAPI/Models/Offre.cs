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

        public int? NombreCandidats { get; set; }

        public DateTime DateOuverture { get; set; } = DateTime.Now;

        public EtatOffre? Etat { get; set; } = EtatOffre.Ouverte;
        
        public List<Proffesion>? Proffesions { get; set; }

        public virtual Admin? Admin { get; set; }
        
        [ForeignKey("Admin")]
        public int? AdminFK { get; set; }

        public virtual List<Candidature>? Candidatures { get; set; }
        public virtual List<Question>? Questions { get; set; }

    }

    public class Question
    {
        public int? QuestionId { get; set; }
        public string? Libelle { get; set; }
        public List<string>? Suggestion { get; set; }
        public bool? EstObligatoire { get; set; }
    }
}
