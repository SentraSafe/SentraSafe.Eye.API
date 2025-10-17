using Microsoft.EntityFrameworkCore;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;
using System.Xml.Schema;


namespace EYEAPI.Contexts
{
    public class EyeContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Location> Locations{ get; set; }
        public DbSet<Sublocation> Sublocations{ get; set; }
        public DbSet<Alarm> Alarms{ get; set; }
        public DbSet<Log> Logs{ get; set; }
        public DbSet<EventLog> EventLogs { get; set; }           
    }
}
