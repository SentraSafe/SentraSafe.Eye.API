using EYEAPI.Models.Enums;

namespace EYEAPI.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sublocation Sublocation { get; set; }
        public MachineType Type { get; set; }
    }
}
