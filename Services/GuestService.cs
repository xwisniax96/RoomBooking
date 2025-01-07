using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;
using RoomBooking.Models;

namespace RoomBooking.Services
{
    public class GuestService : IGuestService
    {
        private readonly ApplicationDbContext _context;

        public GuestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AssignGuestToRoom(Guest guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetAvailableRooms(int requiredCapacity)
        {
            return await _context.Rooms
                .Where(r => r.Capacity >= requiredCapacity) 
                .Where(r => !_context.Guests.Any(g => g.RoomId == r.Id)) 
                .ToListAsync();
        }

        public async Task<List<Guest>> GetGuestsByRoomId(int roomId)
        {
            return await _context.Guests
               .Where(g => g.RoomId == roomId)
               .ToListAsync();
        }

        public async Task<Room> GetRoomById(int roomId)
        {
            return await _context.Rooms.FindAsync(roomId);
        }



    }
}
