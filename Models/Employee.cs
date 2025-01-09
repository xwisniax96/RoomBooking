using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WorkPosition { get; set; }
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }
    }
}
