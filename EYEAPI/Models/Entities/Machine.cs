using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Entities
{
    [AutoMap(typeof(CreateMachineDto))]
    [AutoMap(typeof(UpdateMachineDto))]
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MachineType Type { get; set; }
        public int SublocationId { get; set; }
        public Sublocation Sublocation { get; set; }
        public ICollection<Alarm>? Alarms { get; set; }
        public ICollection<EventLog>? EventLogs { get; set; }
        
        [NotMapped]
        public string? Status { get; set; }
        [NotMapped]
        public EventLog? LatestLog { get; set; }
    }
}
