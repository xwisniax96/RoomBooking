using RoomBooking.Models;
using System.Linq;
using System.Threading.Tasks;
namespace RoomBooking.Services
{
    public interface IRoomService
    {
        Task<List<Room>> GetAll();
        Task<Room> GetById(int id);
        Task AddNewRoom(Room room);
        Task UpdateRoom(Room room);
        Task DeleteRoom(int id);
        Task<List<Room>> GetAllRoomsWithGuests();
    }
}
