using Microsoft.AspNetCore.Mvc;
using RoomBooking.Models;
using RoomBooking.Services;
using System.Diagnostics;

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
            //Check errors 
            /*
                        if (!ModelState.IsValid)
                        {
                            var errors = ModelState.Values.SelectMany(v => v.Errors);
                            foreach (var error in errors)
                            {
                                Debug.WriteLine(error.ErrorMessage); // Wypisuje błędy w konsoli
                            }
                        }*/

            if (string.IsNullOrWhiteSpace(room.Name))
            {
                ModelState.AddModelError("Name", "Nazwa pokoju nie może być pusta.");
            }

            if (room.Capacity < 1)
            {
                ModelState.AddModelError("Capacity", "Pojemność pokoju musi być większa od 0.");
            }


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
