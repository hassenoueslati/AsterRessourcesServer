using System.Text.Json.Serialization;

namespace BackOfficeAPI.Models
{
    public class FacebookUserInfoResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;
        
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;
        
        [JsonPropertyName("photoUrl")]
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
