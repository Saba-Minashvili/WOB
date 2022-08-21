using System.Text.Json.Serialization;

namespace Contracts.FeedBack
{
    public class UpdateFeedBackDto
    {
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }
        [JsonPropertyName("modifiedAt")]
        public string? ModifiedAt { get; set; }
    }
}
