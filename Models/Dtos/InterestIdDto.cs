using System.Text.Json.Serialization;

namespace MiniAPI.Models.Dtos
{
    public class InterestIdDto
    {

        // den skapar jag bara för att koppla personer med intresser
        // känns omständigt? finns det bättre väg?

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
