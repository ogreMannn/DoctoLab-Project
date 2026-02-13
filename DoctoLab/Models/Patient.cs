using DoctoLab.Models.Common;

namespace DoctoLab.Models
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public List<Appointment> Appointments { get; set; } = new();

       
    }
}
