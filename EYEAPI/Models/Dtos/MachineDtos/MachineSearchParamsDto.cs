using EYEAPI.Models.Enums;


namespace EYEAPI.Models.Dtos.MachineDtos
{
    public class MachineSearchParamsDto
    {
        public string? Name { get; set; }
        public int? LocationId { get; set; }
        public int? SublocationId { get; set; }
        public MachineType? MachineType { get; set; }
    }
}
