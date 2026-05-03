using Case_1_Event_eksamen.Pages.Models;
using Case_1_Event_eksamen.Pages.Data;
using Microsoft.EntityFrameworkCore;


namespace Case_1_Event_eksamen.Pages.Services;
public class EventService
{
    private readonly AppDbContexxt _context;

    public EventService(AppDbContexxt context)
    {
        _context = context;
    }

    public bool CreateEvent(EventInput input)
    {
        var newEvent = new Event
        {
            Title = input.Title,
            Description = input.Description,
            StartTime = input.StartTime,
            EndTime = input.EndTime,
            MaxParticipants = input.MaxParticipants
        };

        _context.Event.Add(newEvent);
        _context.SaveChanges();

        return true;
    }

    public List<Event> GetUpcomingEvents()
    {
        return _context.Event
            .Where(e => e.StartTime >= DateTime.Now)
            .OrderBy(e => e.StartTime)
            .ToList();
    }
}
