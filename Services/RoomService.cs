using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;
using RoomBooking.Models;

namespace RoomBooking.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;        
        }
        public async Task AddNewRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetById(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Room>> GetAllRoomsWithGuests()
        {
            return await _context.Rooms
                .Include(r => r.Guests) 
                .ToListAsync();
        }

        public async Task<List<Room>> GetAllRoomsWithGuestsAndHotels()
        {
            return await _context.Rooms
                .Include(r => r.Guests)
                .Include(r => r.Hotel)
                .ToListAsync();
        }
    }
}
