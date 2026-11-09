namespace RacedayFINAL.Models
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public double Distance { get; set; }
        public string EventType { get; set; }
        public string Organiser { get; set; }
        public IEnumerable<string> Categories { get; set; } = new List<string>();
    }
}
    
