using headhunter.Dtos;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace headhunter.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ResumeForUser? Resume { get; set; }
        public int ResumeId { get; set; }
        public string Password { get; set; }
        public string BasketId { get; set; }
    }
}
