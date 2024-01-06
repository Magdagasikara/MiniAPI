using System.Text.Json.Serialization;

namespace MiniAPI.Models.Dtos
{
    public class PersonDto
    {
        [JsonPropertyName("first_name")] 
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
