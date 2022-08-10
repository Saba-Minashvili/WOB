using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class FeedBack : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Comment { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CommentDate { get; set; }
        public int BookId { get; set; }
    }
}
