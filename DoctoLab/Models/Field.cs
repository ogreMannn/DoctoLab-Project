using DoctoLab.Models.Common;

namespace DoctoLab.Models
{
    public class Field : BaseEntity
    {
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
