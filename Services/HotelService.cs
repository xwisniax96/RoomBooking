using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;
using RoomBooking.Models;

namespace RoomBooking.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddHotelAsync(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .Include(h => h.Employees)
                .ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .Include(h => h.Employees)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task UpdateHotelAsync(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }
    }
}
