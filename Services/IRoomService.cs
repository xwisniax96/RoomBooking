using RoomBooking.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;
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
        Task<List<Room>> GetAllRoomsWithGuestsAndHotels();
    }
}


