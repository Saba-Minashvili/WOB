using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class FavouriteBook : BaseEntity
    {
        public Book? Book { get; set; }
        [Required]
        public string? UserId { get; set; }
    }
}
