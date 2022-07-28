using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackOfficeAPI.Models
{
    public enum EtatCandidature
    {
        Actif = 0,
        AExaminer = 1,
        Examine = 2,
        EnCommunication = 3,
        Recrute = 4,
        NonRetenu = 5

    };
    public class Candidature
    {
        public EtatCandidature? Etat { get; set; } = EtatCandidature.Actif;
        public int? PartinenceProfil { get; set; } = 0;
        public DateTime? DateCandidature { get; set; } = DateTime.Now;
        public Boolean? ProfileInteressant { get; set; }

        public virtual Candidat? Candidat { get; set; }
        [ForeignKey("Candidat")]
        public int? CandidatFK { get; set; }
        public virtual Offre? Offre { get; set; }
        [ForeignKey("Offre")]
        public int? OffreFK { get; set; }

    }
}
