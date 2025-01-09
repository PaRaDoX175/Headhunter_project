using System.ComponentModel.DataAnnotations;

namespace headhunter.Entities
{
    public class ContactInformation : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
