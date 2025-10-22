using AutoMapper;
using EYEAPI.Models.Enums;
using EYEAPI.Contexts;
using EYEAPI.Models.Dtos.AlarmDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace EYEAPI.Models.Entities
{
    [AutoMap(typeof(CreateAlarmDto))]
    public class Alarm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public ICollection<EventLog>? EventLogs { get; set; }
        public MeasurementType ValueType { get; set; }
        public Severity Severity { get; set; }
        public int MaximumValue { get; set; }
    }
}
