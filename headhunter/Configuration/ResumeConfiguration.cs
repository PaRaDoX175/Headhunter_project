using headhunter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace headhunter.Configuration
{
    public class ResumeConfiguration : IEntityTypeConfiguration<Resume>
    {
        public void Configure(EntityTypeBuilder<Resume> builder)
        {
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.AboutMe).IsRequired().HasMaxLength(1000);
            builder.HasOne(r => r.ContactInformation).WithMany().HasForeignKey(r => r.ContactInformationId);
            builder.Property(r => r.Profession).IsRequired();
            builder.Property(r => r.Skills).IsRequired();
        }
    }
}
