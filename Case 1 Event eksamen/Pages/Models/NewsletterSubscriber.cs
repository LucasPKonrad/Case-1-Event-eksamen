namespace Case_1_Event_eksamen.Pages.Models
{
    public class NewsletterSubscriber
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime SubscribedAt { get; set; } = DateTime.Now;
    }
}