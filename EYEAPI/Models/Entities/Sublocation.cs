namespace EYEAPI.Models.Entities
{
    public class Sublocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
