using Case_1_Event_eksamen.Pages.Models;

namespace Case_1_Event_eksamen.Pages.Services;
public class EventService
{
    private static readonly List<Event> Events = new();

    public bool CreateEvent(EventInput input)
    {
        var newEvent = new Event
        {
            Id = Events.Count + 1,
            Title = input.Title,
            Description = input.Description,
            StartTime = input.StartTime,
            EndTime = input.EndTime,
            MaxParticipants = input.MaxParticipants
        };

        Events.Add(newEvent);

        Console.WriteLine($"Event created: {newEvent.Title}");
        Console.WriteLine($"Total events: {Events.Count}");

        return true;
    }

    public List<Event> GetAllEvents()
    {
        return Events;
    }
}
