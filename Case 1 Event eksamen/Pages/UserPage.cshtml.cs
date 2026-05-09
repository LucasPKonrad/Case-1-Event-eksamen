using Case_1_Event_eksamen.Pages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Case_1_Event_eksamen.Pages.Models;
using Microsoft.EntityFrameworkCore;
namespace Case_1_Event_eksamen.Pages
{
    public class UserPageModel : PageModel
    {

        private readonly AppDbContexxt _context;

        public UserPageModel(AppDbContexxt context)
        {
            _context = context;
        }
        public List<User> Users { get; set; } = new List<User>();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserName") == null) 
            {
                return RedirectToPage("/Account/Login");
            }

            Users = _context.Users
                .OrderBy(u => u.Role)
                .ThenBy(u => u.Name)
                .ToList();

            return Page();
        }
    }
}
