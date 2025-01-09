using headhunter.Dtos;
using headhunter.Entities;
using Microsoft.EntityFrameworkCore;

namespace headhunter
{
    public interface IAppIdentityDbContext
    {
        DbSet<AppUser> User { get; set; }
        DbSet<ResumeForUser> Resume { get; set; }
    }
}
