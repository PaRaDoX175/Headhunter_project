using headhunter.Dtos;
using headhunter.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace headhunter
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>, IAppIdentityDbContext
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        public AppIdentityDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }

        public DbSet<AppUser> User { get; set; }
        public DbSet<ResumeForUser> Resume { get; set; }
    }
}
