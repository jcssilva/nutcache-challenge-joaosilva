using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Employees.API.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime DateBirth { get; set; }
        
        [Required]
        public string Gender { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression(@"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)", ErrorMessage ="Wrong format, 000.000.000-00")]
        public string CPF { get; set; }
        
        [Required]
        [RegularExpression(@"([0-2][0-9])\/([0-9][0-9][0-9][0-9])", ErrorMessage = "Wrong format, use MM/YYYY")]
        public string StartDate { get; set; }

        [AllowNull]
        public string Team { get; set; } = " ";

    }
}
