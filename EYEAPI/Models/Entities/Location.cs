namespace EYEAPI.Models.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sublocation> Sublocations { get; } = new List<Sublocation>();
    }
}
