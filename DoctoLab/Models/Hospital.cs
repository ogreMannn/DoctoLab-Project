using DoctoLab.Models.Common;

namespace DoctoLab.Models
{
    public class Hospital : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
       
        public List<Doctor> Doctors { get; set; }
    }
}
