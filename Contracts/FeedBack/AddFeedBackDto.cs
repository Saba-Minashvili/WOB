using System.Text.Json.Serialization;

namespace Contracts.FeedBack
{
    public class AddFeedBackDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }
        [JsonPropertyName("bookId")]
        public int BookId { get; set; }
    }
}
