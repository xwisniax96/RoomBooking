using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Imię nie może zawierać znaków specjalnych.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [MaxLength(1000, ErrorMessage = "Opis nie może być dłuższy niż 1000 znaków.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Adres jest wymagany.")]
        [MaxLength(200, ErrorMessage = "Adres nie może być dłuższy niż 200 znaków.")]
        public string Address { get; set; }
        public List<Room> Rooms { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
    }
}