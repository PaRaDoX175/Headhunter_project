using System.ComponentModel.DataAnnotations;

namespace headhunter.Entities
{
    public class Resume : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AboutMe { get; set; }
        public string PictureUrl { get; set; }
        public ContactInformation ContactInformation { get; set; }
        public int ContactInformationId { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string Skills { get; set; }
    }
}
