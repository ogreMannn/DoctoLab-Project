using DoctoLab.Models.Common;

namespace DoctoLab.Models
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }


        public Field field { get; set; }
        public int FieldId { get; set; }

        public Hospital hospital { get; set; }
        public int HospitalId { get; set; }

        public List<Appointment> Appointments { get; set; } = new();
       

    }
}
