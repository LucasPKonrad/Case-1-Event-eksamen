using Case_1_Event_eksamen.Pages.Models;


namespace Case_1_Event_eksamen.Pages.Services
{
    public class UserService
    {
        private readonly PasswordHasher _passwordHasher;
        private static readonly List<User> Users = new();
        public int GetUserCount()
        {
            return Users.Count;
        }

        public User? Login(LoginInput input)
        {
            var user = Users.FirstOrDefault(u => u.Email.ToLower() == input.Email.ToLower());
            if (user == null)
            {
                return null; // User not found
            }

            string hashed = _passwordHasher.HashPassword(input.Password);

            if (user.PasswordHash != hashed)
            {
                return null; // Incorrect password
            }
            return user;
        }

        public UserService(PasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            if (!Users.Any(u => u.Email == "admin@zoo.dk"))
            {
                Users.Add(new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@zoo.dk",
                    PasswordHash = _passwordHasher.HashPassword("admin123"),
                    Role = UserRole.Admin
                });
            }
        }

        
        public bool RegisterUser(RegisterInput input)
        {
            bool emailExists = Users.Any(u =>
                u.Email.Equals(input.Email, StringComparison.OrdinalIgnoreCase));

            if (emailExists)
            {
                return false;
            }

            var user = new User
            {
                Id = Users.Count + 1,
                Name = input.Name,
                Email = input.Email,
                PasswordHash = _passwordHasher.HashPassword(input.Password),
                Role = UserRole.Student
            };

            Users.Add(user);

            Console.WriteLine($"Added user: {user.Email}");
            Console.WriteLine($"Total users: {Users.Count}");

            return true;
        }
    }
    
}
