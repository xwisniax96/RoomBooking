using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;
using RoomBooking.Models;

namespace RoomBooking.Controllers
{
    [Route("Room")]
    [ApiController]
    public class RoomBookingApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Room>>> GetAll()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return Ok(rooms);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Room>> GetById(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }
            return Ok(room);
        }

        [HttpPost("AddNewRoom")]
        public async Task<IActionResult> AddNewRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("UpdateRoom/{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room room)
        {
            if (id != room.Id)
            {
                return BadRequest("Room ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRoom = await _context.Rooms.FindAsync(id);
            if (existingRoom == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }

            existingRoom.Name = room.Name;
            existingRoom.Capacity = room.Capacity;
            existingRoom.HotelId = room.HotelId;
            existingRoom.Guests = room.Guests;

            _context.Rooms.Update(existingRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteRoom/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }
         


        [HttpGet("GetAllRoomsWithGuests")]
        public async Task<ActionResult<List<RoomWithGuestsDTO>>> GetAllRoomsWithGuests()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Guests) 
                .Select(r => new RoomWithGuestsDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    GuestNames = r.Guests.Select(g => g.FirstName).ToList() 
                })
                .ToListAsync();

            return Ok(rooms);
        }


        [HttpGet("GetAllRoomsWithGuestsAndHotels")]
        public async Task<ActionResult<List<RoomDTO>>> GetAllRoomsWithGuestsAndHotels()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Guests)
                .Include(r => r.Hotel)
                .Select(r => new RoomDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    GuestNames = r.Guests.Select(g => g.FirstName).ToList(),
                    HotelName = r.Hotel.Name
                })
                .ToListAsync();

            return Ok(rooms);
        }

    }
}
