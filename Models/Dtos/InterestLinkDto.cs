using System.Text.Json.Serialization;

namespace MiniAPI.Models.Dtos
{
    public class InterestLinkDto
    {
        [JsonPropertyName("link")]
        public string Link { get; set; }
        public virtual Person Person { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
