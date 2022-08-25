using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string? GenreName { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}
