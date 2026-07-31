using System.ComponentModel.DataAnnotations;

namespace PatientFeedbackSystem.Models
{
    public class RegisterViewModel
    {

        [Required]
        public string Name { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        public string Gender { get; set; }


        [Required]
        public int Age { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}