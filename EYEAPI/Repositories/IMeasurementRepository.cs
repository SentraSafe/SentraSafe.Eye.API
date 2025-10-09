using EYEAPI.Models;

namespace EYEAPI.Repositories
{
    public interface IMeasurementRepository
    {
        Task AddMeasurement(Measurement measurement);
        Task<List<Measurement>> GetMeasurements(string? location, long? from, int? measurementType);
        Task<List<string>> GetLocations();
    }
}
