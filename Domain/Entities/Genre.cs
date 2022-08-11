namespace Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string? GenreName { get; set; }
        public int BookId { get; set; }
    }
}
