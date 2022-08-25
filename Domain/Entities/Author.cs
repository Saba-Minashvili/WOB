using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Author : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Origin { get; set; }
        [Required]
        [MaxLength(600)]
        public string? Biography { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [MaxLength(100)]
        public string? DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        [MaxLength(100)]
        public string? DateOfDeath { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}
