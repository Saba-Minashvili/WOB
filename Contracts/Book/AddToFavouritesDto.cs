namespace Contracts.Book
{
    public class AddToFavouritesDto
    {
        public int BookId { get; set; }
        public string? UserId { get; set; }
    }
}
