using System.Text.Json.Serialization;

namespace ETicaretAPI.Application.DTOs.Facebook
{
    public class FacebookUserAccesTokenValidation
    {
        [JsonPropertyName("data")]
        public FacebookUserAccesTokenValidationData Data { get; set; }
    }
    public class FacebookUserAccesTokenValidationData
    {
        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
    
}
