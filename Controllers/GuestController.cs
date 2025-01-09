using Microsoft.AspNetCore.Mvc;
using RoomBooking.Models;
using RoomBooking.Services;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RoomBooking.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }
        [HttpGet]
        public IActionResult GuestNumber()
        {

            return View("~/Views/Guest/GuestNumber.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Search(int capacity)
        {
            if (capacity < 1)
            {
                ModelState.AddModelError("capacity", "Liczba gości musi być większa od 0.");
                return View("FindRoom");
            }

            var availableRooms = await _guestService.GetAvailableRooms(capacity);

            if (!availableRooms.Any())
            {
                ViewBag.Message = "Brak pokoi dla takiej ilości osób";
            }

            return View("AvailableRooms", availableRooms);
        }


        public IActionResult Reserve(int roomId)
        {
            var guest = new Guest { RoomId = roomId };
            return View(guest);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(Guest guest)
        {

            guest.Room = await _guestService.GetRoomById(guest.RoomId);

            if (!Regex.IsMatch(guest.PhoneNumber, @"^\d{9}$"))
            {
                ModelState.AddModelError("PhoneNumber", "Numer telefonu musi składać się z 9 cyfr.");
                return View(guest);
            }

            if (!Regex.IsMatch(guest.Email, @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$"))
            {
                ModelState.AddModelError("Email", "Podano nieprawidłowy adres email.");
                return View(guest);
            }

            if (ModelState.IsValid)
            {
                await _guestService.AssignGuestToRoom(guest);
                return RedirectToAction("Index", "Home");
            }

            return View(guest);
        }

        public async Task<IActionResult> GuestsByRoom(int roomId)
        {
            var guests = await _guestService.GetGuestsByRoomId(roomId);
            return View(guests);
        }
    }
}
