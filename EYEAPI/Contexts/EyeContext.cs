using Microsoft.EntityFrameworkCore;
using EYEAPI.Models.Entities;

namespace EYEAPI.Contexts
{
    public class EyeContext : DbContext
    {
        public EyeContext(DbContextOptions<EyeContext> options) : base(options)
        {
            
        }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Location> Locations{ get; set; }
        public DbSet<Sublocation> Sublocations{ get; set; }
        public DbSet<Alarm> Alarms{ get; set; }
        public DbSet<EventLog> EventLogs { get; set; }           
    }
}
