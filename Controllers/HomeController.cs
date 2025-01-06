using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Models;
using RoomBooking.Services;

namespace RoomBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoomService _roomService;

        public HomeController
        (
            ILogger<HomeController> logger,
            IRoomService roomService
        )
        {
            _logger = logger;
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAll();
            return View(rooms); 
        }

       

        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            await _roomService.DeleteRoom(id); 
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
