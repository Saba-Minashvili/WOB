using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contracts.FeedBack
{
    public class FeedBackDto
    {
        public int Id { get; set; }
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }
        [JsonPropertyName("commentDate")]
        [DataType(DataType.DateTime)]
        public string? CommentDate { get; set; }
        [JsonPropertyName("modifiedAt")]
        [DataType(DataType.DateTime)]
        public string? ModifiedAt { get; set; }
        [JsonPropertyName("bookId")]
        public int BookId { get; set; }
    }
}
