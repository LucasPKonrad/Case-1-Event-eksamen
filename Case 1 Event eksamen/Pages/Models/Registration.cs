namespace Case_1_Event_eksamen.Pages.Models
{
    public class Registration // Represents a registration of a user for an event, including the registration time
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.Now; // Automatically set to current time when created
    }
}
