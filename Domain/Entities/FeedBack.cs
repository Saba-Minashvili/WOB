namespace Domain.Entities
{
    public class FeedBack : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Comment { get; set; }
        public string? CommentDate { get; set; }
        public int BookId { get; set; }
    }
}
