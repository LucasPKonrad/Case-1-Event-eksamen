using Case_1_Event_eksamen.Pages.Data;
using Case_1_Event_eksamen.Pages.Models;


namespace Case_1_Event_eksamen.Pages.Services
{
    public class UserService
    {
        private readonly PasswordHasher _passwordHasher;
        private readonly AppDbContexxt _context;
        public int GetUserCount()
        {
            return _context.Users.Count();
        }

        public User? Login(LoginInput input)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Email.Equals(input.Email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                return null;
            }

            string hashed = _passwordHasher.HashPassword(input.Password);

            if (user.PasswordHash != hashed)
            {
                return null;
            }

            return user;
        }

        public UserService(PasswordHasher passwordHasher, AppDbContexxt context)
        {
            _passwordHasher = passwordHasher;
            _context = context;
        }


        public bool RegisterUser(RegisterInput input)
        {
            bool emailExists = _context.Users.Any(u =>
                u.Email.Equals(input.Email, StringComparison.OrdinalIgnoreCase));

            if (emailExists)
            {
                return false;
            }

            var user = new User
            {
                Name = input.Name,
                Email = input.Email,
                PasswordHash = _passwordHasher.HashPassword(input.Password),
                Role = UserRole.Student
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            Console.WriteLine($"Added user: {user.Email}");
            Console.WriteLine($"Total users: {_context.Users.Count()}");

            return true;
        }
        
    }
    
}
