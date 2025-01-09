using headhunter.Entities;
using Microsoft.EntityFrameworkCore;

namespace headhunter
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Resume> Resumes { get; set; }
        public DbSet<ContactInformation> ContactInformation { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
    }
}
