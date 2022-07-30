using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contracts
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
        public DateTime? CommentDate { get; set; }
        [JsonPropertyName("bookId")]
        public int BookId { get; set; }
    }
}
