using System.ComponentModel.DataAnnotations;
namespace Case_1_Event_eksamen.Pages.Models
{
    public class LoginInput
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
