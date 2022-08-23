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
        [JsonPropertyName("data.is_valid")]
        public bool IsValid { get; set; } 
        [JsonPropertyName("data.user_id")]
        public string UserId { get; set; } = string.Empty;

    }
}
