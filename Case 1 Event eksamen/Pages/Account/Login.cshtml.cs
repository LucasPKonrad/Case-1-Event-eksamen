using Case_1_Event_eksamen.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Case_1_Event_eksamen.Pages.Services;

namespace Case_1_Event_eksamen.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;
        public LoginModel(UserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public LoginInput Input { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public IActionResult OnPost()
        {
            Console.WriteLine("Login Onpost Hit");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userService.Login(Input);

            if (user == null)
            {
                ErrorMessage = "Invalid email or password";
                return Page();
            }

            Console.WriteLine($"User {user.Name} logged in with role {user.Role}");

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            return RedirectToPage("/Index");

        }
        public void OnGet()
        {
            
        }
    }
}
