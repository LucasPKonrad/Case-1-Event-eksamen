using System.Security.Cryptography;
using System.Text;

namespace Case_1_Event_eksamen.Pages.Services
{
    public class PasswordHasher // Service class ansvarlig for at hashe passwords ved hjælp af SHA256 algoritmen, hvilket sikrer, at passwords ikke gemmes i klar tekst i databasen og dermed forbedrer sikkerheden for brugernes oplysninger
    {
        public string HashPassword(string password) // Hashes the input password using SHA256 and returns the hashed value as a Base64 string
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
