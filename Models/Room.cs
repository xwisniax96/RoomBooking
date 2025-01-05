using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }
    }

}