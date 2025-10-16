namespace EYEAPI.Models.Entities
{
    public class Measurement
    {
        public long ReadingTime { get; set; }
        public int MeasurementType { get; set; }
        public string Device { get; set; }
        public double Value { get; set; }
        public string Location { get; set; }
        public int MachineId { get; set; }
    }
}
