using Case_1_Event_eksamen.Pages.Models;
using Case_1_Event_eksamen.Pages.Services;
using Case_1_Event_eksamen.Pages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace Case_1_Event_eksamen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EventService _eventService;
        private readonly AppDbContexxt _db;

        public IndexModel(EventService eventService, AppDbContexxt db)
        {
            _eventService = eventService;
            _db = db;
        }

        public string? UserName { get; set; }
        public string? UserRole { get; set; }
        public List<Event> Events { get; set; } = new();
        public bool SubscribeSuccess { get; set; }

        public void OnGet()
        {
            UserName = HttpContext.Session.GetString("UserName");
            UserRole = HttpContext.Session.GetString("UserRole");
            Events = _eventService.GetUpcomingEvents();
        }

        public IActionResult OnPostSubscribe(string email, [FromServices] EmailService emailService)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                // Gem i databasen
                _db.NewsletterSubscribers.Add(new NewsletterSubscriber { Email = email, SubscribedAt = DateTime.Now });
                _db.SaveChanges();

                // Send bekræftelsesmail
                emailService.SendEmail(email, "Tilmelding til nyhedsbrev", "<p>Tak for din tilmelding til Zealand Zoo Cafes nyhedsbrev!</p>");

                SubscribeSuccess = true;
            }

            Events = _eventService.GetUpcomingEvents();
            return Page();
        }

        public IActionResult OnPostTestEmail([FromServices] EmailService emailService)
        {
            emailService.SendEmail("ejh.emil@gmail.com", "Test", "Mail-systemet virker!");
            return Page();
        }
    }
}
