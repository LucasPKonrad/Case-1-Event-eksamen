namespace Case_1_Event_eksamen.Pages.Models
{
    public class Event 
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? MaxParticipants { get; set; }
        public List<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
