namespace PROGPOE_Frontend.Models
{
    public class ParticipantResultsResponse
    {
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public TimeSpan Time { get; set; }
        public int Placement { get; set; }
        
    }
}
