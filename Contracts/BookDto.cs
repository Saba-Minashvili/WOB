using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contracts
{
    public class BookDto
    {
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("authors")]
        public List<AuthorDto>? Authors { get; set; } = new List<AuthorDto>();
        [JsonPropertyName("genre")]
        public string? Genre { get; set; }
        [JsonPropertyName("pages")]
        public int Pages { get; set; }
        [JsonPropertyName("releaseDate")]
        [DataType(DataType.DateTime)]
        public DateTime? ReleaseDate { get; set; }
        [JsonPropertyName("feedBacks")]
        public List<FeedBackDto>? FeedBacks { get; set; } = new List<FeedBackDto>();
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
    }
}
