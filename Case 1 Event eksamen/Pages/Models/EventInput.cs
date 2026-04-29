using System.ComponentModel.DataAnnotations;

namespace Case_1_Event_eksamen.Pages.Models
{
    public class EventInput
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
