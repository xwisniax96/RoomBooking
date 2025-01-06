using Microsoft.AspNetCore.Mvc;
using RoomBooking.Models;
using RoomBooking.Services;

namespace RoomBooking.Controllers
{
    public class AllRoomsController : Controller
    {
        private readonly IRoomService _roomService;
        public AllRoomsController(IRoomService roomService)
        {
                _roomService = roomService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/AllRooms/Create.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.AddNewRoom(room);
                return RedirectToAction("Index", "Home");
            }
            return View(room);
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
                await _roomService.UpdateRoom(room);
                return RedirectToAction("Index", "Home");
            }
            return View(room);
        }

    }
}
