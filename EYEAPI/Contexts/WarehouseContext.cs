using EYEAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EYEAPI.Contexts;

public class WarehouseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Telemetry> Telemetry { get; set; }
    public DbSet<AnalyticsTelemetry> AnalyticsTelemetry { get; set; }
}