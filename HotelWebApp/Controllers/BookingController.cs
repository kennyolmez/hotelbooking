using ApplicationServices.DTOs;
using ApplicationServices.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelAppLibrary.Data;
using HotelWebApp.Models;
using HotelWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelWebApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly HotelServices sql;
        private readonly IValidator<BookingIndexViewModel> validator;
        public BookingController(HotelServices _sql, IValidator<BookingIndexViewModel> _validator)
        {
            sql = _sql;
            validator = _validator;
        }

        public async Task<IActionResult> Index(BookingIndexViewModel obj)
        {
            BookingIndexViewModel viewModel = new BookingIndexViewModel
            {
                CheckInDate = Convert.ToDateTime(obj.CheckInDate), //parse datetime
                CheckOutDate = Convert.ToDateTime(obj.CheckOutDate),
                RoomType = await sql.GetRoomTypeById(obj.RoomTypeId),
                RoomTypeId = obj.RoomTypeId
            };


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id, string checkInDate, string checkOutDate)
        {
            // It probably makes more sense here to send in only the id and then get the booking and show it to the view
            BookingIndexViewModel viewModel = new BookingIndexViewModel
            {
                CheckInDate = Convert.ToDateTime(checkInDate), //parse datetime
                CheckOutDate = Convert.ToDateTime(checkOutDate),
                RoomType = await sql.GetRoomTypeById(id),
                RoomTypeId = id
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(BookingIndexViewModel booking)
        {
            // Get the room type for this booking based on ID
            booking.RoomType = await sql.GetRoomTypeById(booking.RoomTypeId);

            var result = await validator.ValidateAsync(booking);
            result.AddToModelState(this.ModelState);

            if (ModelState.IsValid)
            {
                await sql.CreateBooking(booking.RoomTypeId, booking.FirstName, booking.LastName,
                    booking.Email, booking.CheckInDate, booking.CheckOutDate);

                return View();
            }

            return View("Index", booking);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Unbook(int bookingId)
        {
            //better solution here?
            ViewBag.Id = bookingId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UnbookRoom(int bookingId)
        {
            await sql.UnbookRoom(bookingId);

            return RedirectToAction("Index", "Home");
        }

    }
 
}
