using System.Text.Json.Serialization;

namespace MiniAPI.Models.Dtos
{
    public class InterestDto
    {
        [JsonPropertyName("title")] 
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        public ICollection<InterestLinkDto> InterestLinks { get; set; }

    }
}
