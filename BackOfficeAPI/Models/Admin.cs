using System.Text.Json.Serialization;

namespace BackOfficeAPI.Models
{
    public class Admin : User
    {
        
        public virtual List<Offre>? Offres { get; set; }
    }
}
