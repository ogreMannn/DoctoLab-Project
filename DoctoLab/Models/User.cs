using Microsoft.AspNetCore.Identity;

namespace DoctoLab.Models
{
    public class User : IdentityUser
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
    }
}
