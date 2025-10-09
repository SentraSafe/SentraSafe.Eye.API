using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EYEAPI.Models;

namespace EYEAPI.Contexts
{
    public class MeasurementContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Measurement> Measurements { get; set; }

    }
}
