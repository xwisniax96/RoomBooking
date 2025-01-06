using BCrypt.Net;

namespace RoomBooking.Services
{
    public class PasswordService
    {
        // Metoda do haszowania hasła
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); // Generowanie hasha
        }

        // Metoda do weryfikacji hasła
        public bool VerifyPassword(string hash, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash); // Porównanie podanego hasła z hash'em
        }
    }
}
