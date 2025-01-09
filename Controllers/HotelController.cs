using Microsoft.AspNetCore.Mvc;
using RoomBooking.Models;
using RoomBooking.Services;

namespace RoomBooking.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IEmployeeService _employeeService;

        public HotelController(IHotelService hotelService , IEmployeeService employeeService)
        {
            _hotelService = hotelService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelService.GetAllHotelsAsync();
            return View(hotels);
        }

        [HttpGet("Hotel/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                await _hotelService.AddHotelAsync(hotel);
                return RedirectToAction("Index", "Hotel");
            }
            return View(hotel);
        }

        [HttpGet("Hotel/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            if (hotel == null) return NotFound();

            var employees = await _employeeService.GetEmployeesByHotelAsync(id);
            ViewBag.Employees = employees;

            return View(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAndBack(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                await _hotelService.UpdateHotelAsync(hotel);
                TempData["SuccessMessage"] = "Dane zostały pomyślnie zmienione.";
                return RedirectToAction("Edit", "Hotel", new { id = hotel.Id });
            }
            else
            {
                TempData["ErrorMessage"] = "Wystąpił błąd podczas aktualizacji danych.";
                return RedirectToAction("Index", "Home");
            }

           
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployeeAsync(employee);
                return RedirectToAction(nameof(Edit), new { id = employee.HotelId });
            }
            return View("Edit", new { id = employee.HotelId });
        }
    }
}
