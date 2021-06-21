using System;
using System.ComponentModel.DataAnnotations;

namespace DotnetCoreRazorWebApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must contain at least 3 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid email format")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        public string PhotoPath { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public Dept? Department { get; set; }
    }
}
