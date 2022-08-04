using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? Photo { get; set; }
        public List<Book>? FavouriteBooks { get; set; } = new List<Book>();
    }
}