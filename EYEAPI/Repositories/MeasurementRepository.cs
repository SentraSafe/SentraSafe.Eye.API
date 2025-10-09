using EYEAPI.Models;
using EYEAPI.Contexts;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;


namespace EYEAPI.Repositories
{
    public class MeasurementRepository(MeasurementContext context) : IMeasurementRepository
    {
        public async Task AddMeasurement(Measurement measurement)
        {
            await context.Measurements.AddAsync(measurement);
            await context.SaveChangesAsync();
        }

        public async Task<List<Measurement>> GetMeasurements(string? location, long? from, int? measurementType)
        {
            IQueryable<Measurement> query = context.Measurements.AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(x => x.Location == location);
            }

            if (from != null)
            {
                query = query.Where(x => x.ReadingTime == from);
            }

            if (measurementType != null)
            {
                query = query.Where(x => x.MeasurementType == measurementType);
            }

            return await query.ToListAsync();
        }

        public async Task<List<string>> GetLocations()
        {
            return await context.Measurements.Select(x => x.Location).Distinct().ToListAsync();
        }
    }
}
