using Microsoft.EntityFrameworkCore;
using Case_1_Event_eksamen.Pages.Models;

namespace Case_1_Event_eksamen.Pages.Data
{
    public class AppDbContexxt : DbContext
    {
        public AppDbContexxt(DbContextOptions<AppDbContexxt> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }


    }
}
