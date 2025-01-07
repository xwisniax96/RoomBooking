using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Models
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Imię może zawierać tylko litery.")]
        [StringLength(100, ErrorMessage = "Imię nie może przekroczyć 100 znaków.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Nazwisko może zawierać tylko litery.")]
        [StringLength(100, ErrorMessage = "Nazwisko nie może przekroczyć 100 znaków.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room? Room { get; set; }
    }
}