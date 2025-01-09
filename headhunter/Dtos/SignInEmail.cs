using headhunter.Entities;

namespace headhunter.Dtos
{
    public class SignInEmail : BaseEntity
    {
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string AppUserId { get; set; }
    }
}
