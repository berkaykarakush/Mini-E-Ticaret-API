using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
