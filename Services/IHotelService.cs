using RoomBooking.Models;

namespace RoomBooking.Services
{
    public interface IHotelService
    {
        Task<List<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task AddHotelAsync(Hotel hotel);
        Task UpdateHotelAsync(Hotel hotel);
    }
}
