using headhunter.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace headhunter.Repository
{
    public class RepositoryIdentity : IRepositoryIdentity
    {
        private readonly UserManager<AppUser> _userManager;

        public RepositoryIdentity(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> GetEntityById(int id)
        {
            return await _userManager.Users.Include(x => x.Resume).FirstOrDefaultAsync(x => x.Resume.Id == id);
        }
    }
}
