using ApplicationServices.DTOs;
using ApplicationServices.Services;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelWebApp.Models;
using HotelWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;

namespace HotelWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HotelServices sql;
        private readonly IValidator<HomeIndexViewModel> validator;

        public HomeController(ILogger<HomeController> logger, HotelServices _sql, IValidator<HomeIndexViewModel> _validator)
        {
            _logger = logger;
            sql = _sql;
            validator = _validator;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Amenities = await sql.GetAmenities();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(HomeIndexViewModel viewModel)
        {
            // Have to do this again or you get object initialization failed because there are no instances of the object in the view
            ViewBag.Amenities = await sql.GetAmenities();

            // How can I better deal with these conversions?
            List<int> amenityIds = (Request.Form["Amenities"]).ToList().ConvertAll(int.Parse); //amenityIds converted to ints

            // Change in service to take the entire ViewModel?
            // Plus use parse datetime
            List<RoomTypeDTO> roomTypes = await sql.GetAvailableRoomTypes(
                    Convert.ToDateTime(viewModel.CheckInDate), Convert.ToDateTime(viewModel.CheckOutDate),
                    Convert.ToDecimal(viewModel.MinPrice), Convert.ToDecimal(viewModel.MaxPrice),
                    amenityIds);

            viewModel.RoomTypes = roomTypes;

            var result = await validator.ValidateAsync(viewModel);
            result.AddToModelState(this.ModelState);

            if (ModelState.IsValid)
            {
                return View(viewModel);
            }

            return View();
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