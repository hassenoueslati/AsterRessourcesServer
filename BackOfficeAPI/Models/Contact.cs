namespace BackOfficeAPI.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string NomPrenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string Sujet { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
