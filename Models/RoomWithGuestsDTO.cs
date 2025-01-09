namespace RoomBooking.Models
{
    public class RoomWithGuestsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<string>? GuestNames { get; set; } 
    }
}
