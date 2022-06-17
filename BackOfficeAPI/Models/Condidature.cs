using System.ComponentModel.DataAnnotations.Schema;

namespace BackOfficeAPI.Models
{
    public enum EtatCondidature
    {
        Actif = 0,
        AExaminer = 1,
        Examine = 2,
        EnCommunication = 3,
        Recrute = 4,
        NonRetenu = 5

    };
    public class Condidature
    {
        public EtatCondidature Etat { get; set; } = EtatCondidature.Actif;
        public int? PartinenceProfil { get; set; } = 0;
        public DateTime DateCondidature { get; set; } = DateTime.Now;
        public Boolean? ProfileInteressant { get; set; }

        public virtual Candidat? Candidat { get; set; }
        [ForeignKey("Candidat")]
        public int CandidatFK { get; set; }
        public virtual Offre? Offre { get; set; }
        [ForeignKey("Offre")]
        public int OffreFK { get; set; }

    }
}
