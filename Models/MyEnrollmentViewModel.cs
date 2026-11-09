namespace RacedayFINAL.Models
{
    public class MyEnrollmentViewModel
    {
        public int EnrollmentId { get; set; }
        public string Event { get; set; }
        public string Category { get; set; }
        public DateTime EventDate { get; set; }
        public string? FinishTime { get; set; }
        public int? Position { get; set; }
    }
}
