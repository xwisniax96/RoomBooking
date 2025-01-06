using Microsoft.AspNetCore.Mvc;
using RoomBooking.Models;
using RoomBooking.Services;
using RoomBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace RoomBooking.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordService _passwordService;

        // Konstruktor, w którym wstrzykujemy zależności
        public AccountController(ApplicationDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        // Akcja rejestracji
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            // Hashujemy hasło
            string hashedPassword = _passwordService.HashPassword(password);

            var newUser = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        // Akcja logowania
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Weryfikujemy hasło
            bool isPasswordValid = _passwordService.VerifyPassword(user.PasswordHash, password);

            if (!isPasswordValid)
            {
                return BadRequest("Invalid password.");
            }

            return Ok("User logged in successfully.");
        }
    }
}
