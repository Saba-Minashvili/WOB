using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class FeedBack : BaseEntity
    {
        public string? Name { get; set; }
        public string? Comment { get; set; }
        [DataType(DataType.DateTime)]
        public string? CommentDate { get; set; }
        [DataType(DataType.DateTime)]
        public string? ModifiedAt { get; set; }
        public int BookId { get; set; }
    }
}
