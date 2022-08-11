namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public List<Author>? Authors { get; set; } = new List<Author>();
        public List<Genre>? Genres { get; set; } = new List<Genre>();
        public int Pages { get; set; }
        public string? Description { get; set; }
        public string? ReleaseDate { get; set; }
        public List<FeedBack>? FeedBacks { get; set; } = new List<FeedBack>();
    }
}
