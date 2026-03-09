using DoctoLab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctoLab.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
            builder.Property(x => x.FilePath).IsRequired().HasMaxLength(256);

            builder.HasData(

                new Doctor
                {
                    Id = 1,
                    Name = "Ali",
                    Surname = "Aliyev",
                    Description = "Cardiologist with 10 years experience",
                    FilePath = "doctor1.jpg",
                    FieldId = 1,
                    HospitalId = 1
                },

                new Doctor
                {
                    Id = 2,
                    Name = "Kamal",
                    Surname = "Mammadov",
                    Description = "Dentist specialist",
                    FilePath = "doctor2.jpg",
                    FieldId = 2,
                    HospitalId = 2
                }

            );
        }
    }
}