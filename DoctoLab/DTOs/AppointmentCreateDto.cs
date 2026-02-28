namespace DoctoLab.DTOs
{
    public class AppointmentCreateDto
    {
        public DateTime AppointmentDate { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
