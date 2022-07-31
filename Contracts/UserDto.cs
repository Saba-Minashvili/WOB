namespace Contracts
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Photo { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public List<BookDto>? FavouriteBooks { get; set; } = new List<BookDto>();
    }
}
