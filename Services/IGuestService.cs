using RoomBooking.Models;

namespace RoomBooking.Services
{
    public interface IGuestService
    {
        Task<List<Room>> GetAvailableRooms(int requiredCapacity);
        Task AssignGuestToRoom(Guest guest);
        Task<List<Guest>> GetGuestsByRoomId(int roomId);
        Task<Room> GetRoomById(int roomId);
    }
}
