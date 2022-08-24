using System.Text.Json.Serialization;

namespace BackOfficeAPI.Models
{
    public class FacebookUserAccessTokenValidation
    {
        [JsonPropertyName("data")]
        public FacebookUserAccessTokenValidationData Data { get; set; } = new FacebookUserAccessTokenValidationData();

    }

    public class FacebookUserAccessTokenValidationData
    {
        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; } 
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;

    }
}
