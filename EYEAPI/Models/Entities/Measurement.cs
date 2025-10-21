using MongoDB.Bson.Serialization.Attributes;

namespace EYEAPI.Models.Entities
{
    public class Measurement
    {
        [BsonElement("readingTime")]
        public long ReadingTime { get; set; }
        [BsonElement("measurementType")]
        public int MeasurementType { get; set; }
        [BsonElement("device")]
        public string Device { get; set; }
        [BsonElement("value")]
        public double Value { get; set; }
        [BsonElement("location")]
        public string Location { get; set; }
        [BsonElement("machineId")]
        public int MachineId { get; set; }
    }
}
