using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        public List<Guest>? Guests { get; set; }

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }

    }
}

