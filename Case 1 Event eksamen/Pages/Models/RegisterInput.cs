using System.ComponentModel.DataAnnotations;

namespace Case_1_Event_eksamen.Pages.Models
{
    public class RegisterInput // Representere de data, der kræves for at registrere en ny bruger, inklusive valideringsattributter for at sikre, at de nødvendige oplysninger er korrekt indtastet
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;
    }
}
