using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
        public string? Photo { get; set; }
        public List<FavouriteBook>? FavouriteBooks { get; set; } = new List<FavouriteBook>();
    }
}