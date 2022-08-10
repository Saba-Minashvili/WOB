using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("origin")]
        public string? Origin { get; set; }
        [JsonPropertyName("biography")]
        public string? Biography { get; set; }
        [JsonPropertyName("dateOfBirth")]
        [DataType(DataType.Date)]
        public string? DateOfBirth { get; set; }
        [JsonPropertyName("dateOfDeath")]
        [DataType(DataType.Date)]
        public string? DateOfDeath { get; set; }
        [JsonPropertyName("bookId")]
        public int BookId { get; set; }
    }
}
