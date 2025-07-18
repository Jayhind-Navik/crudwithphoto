using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_Ducat.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only alphabetic characters allowed.")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public IFormFile PhotoUpload { get; set; }

        // Add PhotoPath here to allow access in Edit view
        [NotMapped]
        public string PhotoPath { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
    }

}
