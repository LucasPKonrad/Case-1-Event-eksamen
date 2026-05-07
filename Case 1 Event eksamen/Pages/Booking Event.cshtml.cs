using Case_1_Event_eksamen.Pages.Data;
using Case_1_Event_eksamen.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Case_1_Event_eksamen.Pages
{
    public class Eventbooking
    {
        private readonly AppDbContexxt _context;
        public Eventbooking(AppDbContexxt context)
        {
            _context = context;
        }

        public void GemEvent(Event nyEvent)
        {
            _context.Events.Add(nyEvent);
            _context.SaveChanges();
        }

        public void SletEvent(int id)
        {
            var ev = _context.Events.Find(id);
            if (ev != null)
            {
                _context.Events.Remove(ev);
                _context.SaveChanges();
            }
        }

        public List<Event> HentEventsForMåned(int år, int måned)
        {
            return _context.Events
                .Include(e => e.Registrations)
                .Where(e => e.StartTime.Year == år && e.StartTime.Month == måned)
                .ToList();
        }

        public string TilmeldEvent(int eventId, int userId)
        {
            var ev = _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefault(e => e.Id == eventId);

            if (ev == null) return "Event ikke fundet.";

            bool erAlleredeTilmeldt = ev.Registrations.Any(r => r.UserId == userId);
            if (erAlleredeTilmeldt) return "Du er allerede tilmeldt dette event.";

            if (ev.MaxParticipants.HasValue && ev.Registrations.Count >= ev.MaxParticipants.Value)
                return "Dette event er desværre fuldt.";

            _context.Registrations.Add(new Registration
            {
                EventId = eventId,
                UserId = userId,
                RegisteredAt = DateTime.Now
            });
            _context.SaveChanges();
            return "ok";
        }

        public void FraMeldEvent(int eventId, int userId)
        {
            var reg = _context.Registrations
                .FirstOrDefault(r => r.EventId == eventId && r.UserId == userId);
            if (reg != null)
            {
                _context.Registrations.Remove(reg);
                _context.SaveChanges();
            }
        }
    }
}

namespace Case_1_Event_eksamen.Pages
{
    public class EventModel : PageModel
    {
        private readonly Eventbooking _eventbooking;

        public EventModel(Eventbooking eventbooking)
        {
            _eventbooking = eventbooking;
        }

        [BindProperty]
        public Event NyEvent { get; set; } = new();

        public string? BekræftelseBesked { get; set; }
        public List<Event> MånedsEvents { get; set; } = new();
        public int IndloggetBrugerId { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToPage("/Account/Login");

            IndloggetBrugerId = HttpContext.Session.GetInt32("UserId") ?? 0;

            int måned = int.TryParse(Request.Query["måned"], out int m) ? m : DateTime.Today.Month;
            int år = int.TryParse(Request.Query["år"], out int y) ? y : DateTime.Today.Year;
            MånedsEvents = _eventbooking.HentEventsForMåned(år, måned);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToPage("/Account/Login");

            IndloggetBrugerId = HttpContext.Session.GetInt32("UserId") ?? 0;
            int måned = NyEvent.StartTime != default ? NyEvent.StartTime.Month : DateTime.Today.Month;
            int år = NyEvent.StartTime != default ? NyEvent.StartTime.Year : DateTime.Today.Year;
            MånedsEvents = _eventbooking.HentEventsForMåned(år, måned);

            if (string.IsNullOrWhiteSpace(NyEvent.Title))
            {
                ModelState.AddModelError("", "Skriv venligst et eventnavn!");
                return Page(); 
            }

            if (NyEvent.EndTime <= NyEvent.StartTime)
            {
                ModelState.AddModelError("", "Sluttidspunkt skal være efter starttidspunkt!");
                return Page(); 
            }

            
            _eventbooking.GemEvent(NyEvent);

            MånedsEvents = _eventbooking.HentEventsForMåned(NyEvent.StartTime.Month, NyEvent.StartTime.Year);
            BekræftelseBesked = $"'{NyEvent.Title}' er gemt d. {NyEvent.StartTime:d/M/yyyy} kl. {NyEvent.StartTime:HH:mm} - {NyEvent.EndTime:HH:mm}";
            return Page();
        }

        public IActionResult OnPostSlet(int id, int måned, int år)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToPage("/Account/Login");

            _eventbooking.SletEvent(id);
            return RedirectToPage(new { måned, år });
        }

        public IActionResult OnPostTilmeld(int eventId, int måned, int år)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToPage("/Account/Login");

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var resultat = _eventbooking.TilmeldEvent(eventId, userId);

            TempData["Besked"] = resultat == "ok" ? "✓ Du er nu tilmeldt!" : $"⚠ {resultat}";
            return RedirectToPage(new { måned, år });
        }

        public IActionResult OnPostFraMeld(int eventId, int måned, int år)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToPage("/Account/Login");

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            _eventbooking.FraMeldEvent(eventId, userId);

            TempData["Besked"] = "✓ Du er frammeldt eventet.";
            return RedirectToPage(new { måned, år });
        }
    }
}

