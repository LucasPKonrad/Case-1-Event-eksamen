using System.ComponentModel.DataAnnotations;
namespace Case_1_Event_eksamen.Pages.Models
{
    public class LoginInput // Representere de data, der kræves for at logge ind, inklusive valideringsattributter for at sikre, at de nødvendige oplysninger er korrekt indtastet
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
