namespace RacedayFINAL.Models
{
    public class EventDTO
    {
      
       
         public int EventId { get; set; } // <--- ADD THIS LINE
       
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public double Distance { get; set; }
        public string EventType { get; set; }
    }
}
