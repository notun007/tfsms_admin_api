using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TFSMS.Admin.Models
{
    public class DeviceNumberValidationResponse
    {
        [JsonPropertyName("APIVersion")]
        public string APIVersion { get; set; }

        [JsonPropertyName("Code")]
        public string Code { get; set; }
        [JsonPropertyName("Message")]
        public string Message { get; set; }
        [JsonPropertyName("Status")]
        public string Status { get; set; }

        
    }
}
