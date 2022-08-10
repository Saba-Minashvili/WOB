namespace Domain.Entities
{
    public class FavouriteBook : BaseEntity
    {
        public Book? Book { get; set; }
        public string? UserId { get; set; }
    }
}
