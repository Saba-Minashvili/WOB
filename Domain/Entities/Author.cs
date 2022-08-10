using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Author : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? Origin { get; set; }
        public string? Biography { get; set; }
        [DataType(DataType.Date)]
        public string? DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        public string? DateOfDeath { get; set; }    
        public int BookId { get; set; }
    }
}
