namespace Case_1_Event_eksamen.Pages.Models
{
    public class User // Represents a user in the system, including their credentials and role, as well as their associated registrations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; } = UserRole.Student;
        public List<Registration> Registrations { get; set; } = new(); // Navigation property for the registrations associated with this user

    }
}
