using headhunter.Dtos;
using headhunter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace headhunter.Interfaces
{
    public interface IAccountController
    {
        Task<ActionResult<UserDto>> UserRegister(AppUser user);
        Task<ActionResult<UserDto>> UserLogin(LoginDto login);
        Task<UserDto> GetCurrentUser();
        Task<AppUser> GetUserByEmail(string email);
    }
}
