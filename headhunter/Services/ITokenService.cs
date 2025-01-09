using headhunter.Entities;

namespace headhunter.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        bool IsTokenExpired(string token);
    }
}
