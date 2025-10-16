using EYEAPI.Models.Enums;
using EYEAPI.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace EYEAPI.Models.Entities
{
    public class Alarm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public MachineType MachineType { get; set; }
        public Severity Severity { get; set; }
        public int MaximumValue { get; set; }
    }
}
