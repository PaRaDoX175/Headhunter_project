using System.ComponentModel.DataAnnotations;

namespace headhunter.Dtos
{
    public class RegisterDto
    {
        public ResumeForUser Resume { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string BasketId { get; set; }
    }
}
