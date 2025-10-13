using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Entities
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MachineType Type { get; set; }
        public int SublocationId { get; set; }
        public Sublocation Sublocation { get; set; }
    }
}
