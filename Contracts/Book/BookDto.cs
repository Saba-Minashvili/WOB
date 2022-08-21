using Contracts.FeedBack;
using Contracts.Genre;
using System.Text.Json.Serialization;

namespace Contracts.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
        [JsonPropertyName("authors")]
        public List<AuthorDto>? Authors { get; set; } = new List<AuthorDto>();
        [JsonPropertyName("genres")]
        public List<GenreDto>? Genres { get; set; } = new List<GenreDto>();
        [JsonPropertyName("pages")]
        public int Pages { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("releaseDate")]
        public string? ReleaseDate { get; set; }
        [JsonPropertyName("feedBacks")]
        public List<FeedBackDto>? FeedBacks { get; set; } = new List<FeedBackDto>();
    }
}
