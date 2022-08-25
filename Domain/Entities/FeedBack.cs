using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class FeedBack : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }
        [Required]
        [MaxLength(600)]
        public string? Comment { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [MaxLength(100)]
        public string? CommentDate { get; set; }
        [DataType(DataType.DateTime)]
        [MaxLength(100)]
        public string? ModifiedAt { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}
