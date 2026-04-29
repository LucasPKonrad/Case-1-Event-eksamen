using System.Security.Cryptography;
using System.Text;

namespace Case_1_Event_eksamen.Pages.Services
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
