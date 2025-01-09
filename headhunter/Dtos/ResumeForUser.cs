using System.ComponentModel.DataAnnotations;
using headhunter.Entities;

namespace headhunter.Dtos
{
    public class ResumeForUser : BaseEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string AboutMe { get; set; }

        public string PictureUrl { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Profession { get; set; }

        [Required]
        public string Skills { get; set; }
    }
}
