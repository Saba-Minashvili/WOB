using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contracts
{
    public class AuthorDto
    {
        public int Id { get; set; }
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("origin")]
        public string? Origin { get; set; }
        [JsonPropertyName("biography")]
        public string? Biography { get; set; }
        [JsonPropertyName("dateOfBirth")]
        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }
        [JsonPropertyName("dateOfDeath")]
        [DataType(DataType.DateTime)]
        public DateTime? DateOfDeath { get; set; }
        [JsonPropertyName("bookId")]
        public int BookId { get; set; }
    }
}
