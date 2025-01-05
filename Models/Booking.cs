﻿using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }
    }
}