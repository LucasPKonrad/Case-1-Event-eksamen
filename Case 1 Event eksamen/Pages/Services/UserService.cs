using Case_1_Event_eksamen.Pages.Data;
using Case_1_Event_eksamen.Pages.Models;


namespace Case_1_Event_eksamen.Pages.Services // Service class ansvarlig for håndtering af brugerrelaterede operationer, såsom login og registrering, ved hjælp af PasswordHasher for sikker passwordhåndtering og AppDbContexxt for databaseadgang
{
    public class UserService
    {
        private readonly PasswordHasher _passwordHasher; // Dependency injection of PasswordHasher for secure password handling
        private readonly AppDbContexxt _context; // Dependency injection of AppDbContexxt for database access
        public int GetUserCount()
        {
            return _context.Users.Count();
        }

        public User? Login(LoginInput input) // Returnerer null hvis login mislykkes
        {
            string email = input.Email.ToLower();

            var user = _context.Users.FirstOrDefault(u =>
                u.Email.ToLower() == email);

            if (user == null)
            {
                return null;
            }

            string hashed = _passwordHasher.HashPassword(input.Password); // Hash det indtastede password

            if (user.PasswordHash != hashed)
            {
                return null;
            }

            return user;
        }

        public UserService(PasswordHasher passwordHasher, AppDbContexxt context) // Dependency injection af PasswordHasher og AppDbContexxt
        {
            _passwordHasher = passwordHasher;
            _context = context;
        }


        public bool RegisterUser(RegisterInput input) // Returnerer false hvis email allerede findes, ellers opretter en ny bruger og returnerer true
        {
            string email = input.Email.ToLower();

            bool emailExists = _context.Users.Any(u =>
                u.Email.ToLower() == email);

            if (emailExists)
            {
                return false;
            }

            var user = new User // Opretter en ny User-objekt med de indtastede oplysninger og hashed password
            {
                Name = input.Name,
                Email = input.Email,
                PasswordHash = _passwordHasher.HashPassword(input.Password),
                Role = UserRole.Student
            };

            _context.Users.Add(user); // Tilføjer den nye bruger til databasekonteksten
            _context.SaveChanges();

            Console.WriteLine($"Added user: {user.Email}");
            Console.WriteLine($"Total users: {_context.Users.Count()}");
            return true;
        }
        
    }
    
}
