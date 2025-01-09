using headhunter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace headhunter.Configuration
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.Property(x => x.Position).IsRequired();
            builder.Property(x => x.Department).IsRequired();
            builder.Property(x => x.Location).IsRequired();
            builder.Property(x => x.Requirements).IsRequired();
            builder.Property(x => x.Email).IsRequired();
        }
    }
}
