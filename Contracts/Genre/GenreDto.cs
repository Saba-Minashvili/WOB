using System.Text.Json.Serialization;

namespace Contracts.Genre
{
    public class GenreDto
    {
        public int Id { get; set; }
        [JsonPropertyName("genreName")]
        public string? GenreName { get; set; }
        public int BookId { get; set; }
    }
}
