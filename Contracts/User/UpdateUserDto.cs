using System.Text.Json.Serialization;

namespace Contracts.User
{
    public class UpdateUserDto
    {
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("photo")]
        public string? Photo { get; set; }
    }
}
