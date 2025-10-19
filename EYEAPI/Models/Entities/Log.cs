using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EYEAPI.Models.Entities
{
    [AutoMap(typeof(CreateLogDto))]
    public class Log
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public Severity Severity { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public int? AlarmId { get; set; }
        [ForeignKey(nameof(AlarmId))]
        public Alarm? Alarm { get; set; }
        public string? Value { get; set; }
        public bool IsHandled { get; set; }
        public string? HandledBy { get; set; }
        public DateTime? HandleTime { get; set; }
        public string? HandleDescription { get; set; }
    }
}
