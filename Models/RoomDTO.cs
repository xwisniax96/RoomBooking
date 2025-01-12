﻿namespace RoomBooking.Models
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<string>? GuestNames { get; set; } 
        public string? HotelName { get; set; } 
    }

}
