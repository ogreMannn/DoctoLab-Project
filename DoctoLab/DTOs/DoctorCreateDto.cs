namespace DoctoLab.DTOs
{
    public class DoctorCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }

        public int FieldId { get; set; }
        public int HospitalId { get; set; }
    }
}
