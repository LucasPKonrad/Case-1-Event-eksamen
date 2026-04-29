using Case_1_Event_eksamen.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace Case_1_Event_eksamen.Pages
{
    public class IndexModel : PageModel
    {
        public string? UserName { get; set; }
        public string? UserRole { get; set; }
        public void OnGet()
        {
            UserName = HttpContext.Session.GetString("UserName");
            UserRole = HttpContext.Session.GetString("UserRole");

            Console.WriteLine($"Index Username: {UserName}, UserRole: {UserRole}");

        }
    }
}
