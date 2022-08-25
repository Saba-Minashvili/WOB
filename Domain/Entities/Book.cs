using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public string? Image { get; set; }
        public List<Author>? Authors { get; set; } = new List<Author>();
        public List<Genre>? Genres { get; set; } = new List<Genre>();
        [Required]
        public int Pages { get; set; }
        [Required]
        [MaxLength(600)]
        public string? Description { get; set; }
        [Required]
        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? ReleaseDate { get; set; }
        public List<FeedBack>? FeedBacks { get; set; } = new List<FeedBack>();
    }
}
