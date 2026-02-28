namespace DoctoLab.DTOs
{
    public class AppointmentGetDto
    {
        public int Id { get; set; }
        public DateTime AppointmentData { get; set; }
        public string Status { get; set; }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        
        public int PatientId { get; set; }
        public string PatientName { get; set; }

     
    }
}
