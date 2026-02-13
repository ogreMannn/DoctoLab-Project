using DoctoLab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctoLab.Configurations
{
        public class PatientConfiguration : IEntityTypeConfiguration<Patient>
        {
            public void Configure(EntityTypeBuilder<Patient> builder)
            {
                builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
                builder.Property(x => x.Surname).IsRequired().HasMaxLength(256);
            }
        }
    
}
