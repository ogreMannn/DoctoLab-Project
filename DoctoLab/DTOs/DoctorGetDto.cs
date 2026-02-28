namespace DoctoLab.DTOs
{
    public class DoctorGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }

        public int FieldId { get; set; }
        public string FieldName { get; set; }

        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
    }
}
