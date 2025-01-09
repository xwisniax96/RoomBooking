using RoomBooking.Models;

namespace RoomBooking.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesByHotelAsync(int hotelId);
        Task AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task UpdateEmployeeAsync(Employee employee);
    }
}
