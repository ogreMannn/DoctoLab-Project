using DoctoLab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctoLab.Configurations
{
    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasData(

                new Field
                {
                    Id = 1,
                    Name = "Cardiology"
                },

                new Field
                {
                    Id = 2,
                    Name = "Dentistry"
                },

                new Field
                {
                    Id = 3,
                    Name = "Neurology"
                }

            );
        }
    }
}