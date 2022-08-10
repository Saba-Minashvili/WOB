using System.Text.Json.Serialization;

namespace Contracts.Book
{
    public class AddBookDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
        [JsonPropertyName("authors")]
        public List<AuthorDto>? Authors { get; set; } = new List<AuthorDto>();
        [JsonPropertyName("genre")]
        public string? Genre { get; set; }
        [JsonPropertyName("pages")]
        public int Pages { get; set; }
        [JsonPropertyName("releaseDate")]
        public string? ReleaseDate { get; set; }
    }
}
