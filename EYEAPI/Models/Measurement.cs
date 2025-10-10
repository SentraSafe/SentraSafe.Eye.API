namespace EYEAPI.Models
{
    public class Measurement
    {
        public Int64 ReadingTime { get; set; }
        public int MeasurementType { get; set; }
        public string Device { get; set; }
        public double Value { get; set; }
        public string Location { get; set; } 
    }
}
