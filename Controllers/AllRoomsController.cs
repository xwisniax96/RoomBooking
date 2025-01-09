using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomBooking.Models;
using RoomBooking.Services;
using System.Diagnostics;

namespace RoomBooking.Controllers
{
    public class AllRoomsController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;
        public AllRoomsController(IRoomService roomService ,IHotelService hotelService)
        {
                _roomService = roomService;
                _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var hotels = await _hotelService.GetAllHotelsAsync();
            ViewBag.Hotels = new SelectList(hotels, "Id", "Name");
            return View("~/Views/AllRooms/Create.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            if (string.IsNullOrWhiteSpace(room.Name))
            {
                ModelState.AddModelError("Name", "Nazwa pokoju nie może być pusta.");
            }

            if (room.Capacity < 1)
            {
                ModelState.AddModelError("Capacity", "Pojemność pokoju musi być większa od 0.");
            }

            if (room.HotelId <= 0)
            {
                ModelState.AddModelError("HotelId", "Musisz wybrać hotel.");
            }

            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    Debug.WriteLine($"{state.Key}: {state.Value.Errors.FirstOrDefault()?.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                await _roomService.AddNewRoom(room);
                return RedirectToAction("Index", "Home");
            }

            var hotels = await _hotelService.GetAllHotelsAsync();
            ViewBag.Hotels = new SelectList(hotels, "Id", "Name");
            return View("~/Views/AllRooms/Create.cshtml");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                room.HotelId = id;

                await _roomService.UpdateRoom(room);
                return RedirectToAction("Index", "Home");
            }
            return View(room);
        }

    }
}
