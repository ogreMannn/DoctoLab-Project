using DoctoLab.Models.Common;

namespace DoctoLab.Models
{
    public class Appointment : BaseEntity
    {
        public AppointmentStatus Status { get; set; }
        public DateTime AppointmentDate { get; set; }


        public Doctor Doctor{ get; set; }
        public int DoctorId { get; set; }

        public Patient Patient { get; set; }
        public int PatientId { get; set; }
    }
}
