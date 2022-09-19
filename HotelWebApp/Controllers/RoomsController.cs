using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using HotelWebApp.Models;
using Microsoft.EntityFrameworkCore;
using HotelWebApp.ViewModels;
using ApplicationServices.DTOs;
using ApplicationServices.Services;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace HotelWebApp.Controllers
{
    public class RoomsController : Controller
    {
        private readonly HotelServices sql;
        public OperationsModel operations = new OperationsModel();
        private readonly IValidator<RoomsInfoViewModel> validator;

        public RoomsController(HotelServices _sql, IValidator<RoomsInfoViewModel> _validator)
        {
            sql = _sql;
            validator = _validator;
         }

        // Rooms index page, displays a list of rooms with some basic info
        public async Task<IActionResult> Index()
        {
            // Using a list here because there are lists of reviews and their counts + ratings attached to each unique room
            List<RoomsIndexViewModel> roomReviewViewModel = new List<RoomsIndexViewModel>();

            var roomTypes = await sql.GetAllRoomTypes();

            // Must populate RoomsIndexViewModel with a room + all the reviews for that room

            foreach (var roomType in roomTypes)
            {
                var reviews = await sql.GetReviewsByRoomTypeId(roomType.Id);

                roomReviewViewModel.Add(new RoomsIndexViewModel
                {
                    RoomType = roomType,
                    ReviewCount = reviews.Count(),
                    Rating = operations.RoundRatings(reviews, roomType.Id) // Rounds the ratings
                });
            }
       
            return View(roomReviewViewModel);
        }


        public async Task<IActionResult> Info(RoomsInfoViewModel obj)
        {
            obj.RoomType = await sql.GetRoomTypeById(obj.RoomTypeId);
            obj.Reviews = await sql.GetReviewsByRoomTypeId(obj.RoomTypeId);

            return View(obj);
        }

        // Apparently my POST looks like what a GET should look like
        // and no url tempalte in HttpPost?
        // Making the signature invalid?

        [HttpPost]
        public async Task<IActionResult> Info(int roomTypeId)
        {
            RoomsInfoViewModel viewModel = new RoomsInfoViewModel
            {
                RoomType = await sql.GetRoomTypeById(roomTypeId),
                Reviews = await sql.GetReviewsByRoomTypeId(roomTypeId)
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostReview(RoomsInfoViewModel obj)
        {
            var result = await validator.ValidateAsync(obj);
            result.AddToModelState(this.ModelState);

            if (ModelState.IsValid) // I.e. if the user has posted a description and rating and within the constraints
            {
                await sql.CreateReview(obj.Description, obj.Rating, obj.UserEmail, obj.RoomTypeId);

                return RedirectToAction("Info", obj);
            }

            // Need to get the reviews again for page refresh, where the updated reviews are posted.
            obj.Reviews = await sql.GetReviewsByRoomTypeId(obj.RoomTypeId);
            obj.RoomType = await sql.GetRoomTypeById(obj.RoomTypeId);

            return View("Info", obj);
        }
    }
}
