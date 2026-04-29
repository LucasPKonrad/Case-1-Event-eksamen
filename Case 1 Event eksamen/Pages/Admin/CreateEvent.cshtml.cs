using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Case_1_Event_eksamen.Pages.Models;
using Case_1_Event_eksamen.Pages.Services;
using Case_1_Event_eksamen.Pages.Admin;

namespace Case_1_Event_eksamen.Pages.Admin
{
    public class CreateEventModel : PageModel
    {
        private readonly EventService _eventService;

        public CreateEventModel(EventService eventService)
        {
            _eventService = eventService;
        }

        [BindProperty]
        public EventInput Input { get; set; } = new EventInput();

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/Account/Login");
            }
            

            bool succes = _eventService.CreateEvent(Input);

            if (!succes)
            {
                ErrorMessage = "Could not create event.";
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
