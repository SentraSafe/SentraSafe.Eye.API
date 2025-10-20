using AutoMapper;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.AlarmDtos
{
    [AutoMap(typeof(Alarm))]
    public class AlarmDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? MachineId { get; set; }
        public MachineDto? Machine { get; set; }
        public List<LogDto>? Logs { get; set; }
        public int ValueType { get; set; }
        public Severity Severity { get; set; }
        public int MaximumValue { get; set; }
    }
}
