using System.ComponentModel.DataAnnotations;

namespace Case_1_Event_eksamen.Pages.Models
{
    public class EventInput // Representere de data, der kræves for at oprette en ny begivenhed, inklusive valideringsattributter for at sikre, at de nødvendige oplysninger er korrekt indtastet
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public int? MaxParticipants { get; set; }

    }
}
