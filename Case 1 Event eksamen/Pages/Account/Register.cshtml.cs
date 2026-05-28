using Case_1_Event_eksamen.Pages.Data;
using Case_1_Event_eksamen.Pages.Models;
using Case_1_Event_eksamen.Pages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace Case_1_Event_eksamen.Pages.Account
{

    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;
        private readonly AppDbContexxt _context;

        public RegisterModel(UserService userService, AppDbContexxt context)
        {
            _userService = userService;
            _context = context;
        }

        [BindProperty]
        public RegisterInput Input { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            Console.WriteLine("REGISTER ONPOST HIT");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("MODELSTATE INVALID");
                return Page();
            }

            bool success = _userService.RegisterUser(Input);

            Console.WriteLine($"Register success: {success}");
            Console.WriteLine($"User count after register: {_userService.GetUserCount()}");

            if (!success)
            {
                ErrorMessage = "A user with this email already exists.";
                return Page();
            }

            if (Input.TilmeldNyhedsbrev)
            {
                bool eksisterer = _context.NewsletterSubscribers.Any(s => s.Email == Input.Email);
                if (!eksisterer)
                {
                    _context.NewsletterSubscribers.Add(new NewsletterSubscriber { Email = Input.Email });
                    _context.SaveChanges();
                }
            }

            return RedirectToPage("/Account/Login");
        }
    }
}
