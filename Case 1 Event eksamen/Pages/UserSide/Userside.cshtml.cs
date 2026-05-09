using Case_1_Event_eksamen.Pages.Models;
using Case_1_Event_eksamen.Pages.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Case_1_Event_eksamen.Pages.UserSide;

public class UsersideModel : PageModel
{
    private readonly UserService _userService;
    public List<User> Users { get; set; } = new();
    public List<User> Admins { get; set; } = new();
    public List<User> Students { get; set; } = new();

    [BindProperty] public int EditId { get; set; }
    [BindProperty] public string EditName { get; set; }
    [BindProperty] public string EditEmail { get; set; }

    public UsersideModel(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult OnPost()
    {
        bool edit = _userService.UpdateUser(EditId, EditName, EditEmail);
        return RedirectToPage();
    }
    public void OnGet()
    {
        Users = _userService.GetAllUsers().ToList();
        Admins = Users.Where(u => u.Role == UserRole.Admin).ToList();
        Students = Users.Where(u => u.Role == UserRole.Student).ToList();
    }
}
