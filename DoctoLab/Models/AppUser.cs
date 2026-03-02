using Microsoft.AspNetCore.Identity;

namespace DoctoLab.Models
{
    public class AppUser : IdentityUser
    {
        public string Role { get; set; } = nameof(UserRole.Patient);
        public int? DoctorId { get; set; } 
        public int? PatientId { get; set; }
    }
}
