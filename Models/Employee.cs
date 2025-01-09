using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Imię nie może zawierać znaków specjalnych ani cyfr.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon jest wymagany.")]
        [RegularExpression(@"^\+?[0-9]{9,15}$", ErrorMessage = "Podaj poprawny numer telefonu z kierunkowym (np. +48123456789).")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Stanowisko jest wymagane.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Stanowisko nie może zawierać znaków specjalnych ani cyfr.")]
        public string WorkPosition { get; set; }
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }
    }
}