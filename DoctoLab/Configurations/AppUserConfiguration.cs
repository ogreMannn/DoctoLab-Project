using DoctoLab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DoctoLab.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        void IEntityTypeConfiguration<AppUser>.Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Role).IsRequired().HasMaxLength(20);
            builder.HasOne<Doctor>().WithMany().HasForeignKey().IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Patient>().WithMany().HasForeignKey().IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        }

    }
}
