using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using ApplicationServices.DTOs;
using ApplicationServices.Services;

namespace HotelWebApp.Areas.Identity.Pages.Account.Manage
{
    public class ReviewsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly HotelServices sql;

        public ReviewsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            HotelServices _sql)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            sql = _sql;
        }

        [BindProperty]
        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

        private async Task LoadAsync(List<ReviewDTO> reviews, ApplicationUser user)
        {
            var userId = await _userManager.GetUserIdAsync(user);

            Reviews = await sql.GetReviewsByUserEmail(User.Identity.Name);
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var reviews = Reviews;

            ViewData["RoomTypes"] = await sql.GetAllRoomTypes();


            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(reviews, user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string reviewDescription, int rating, int roomTypeId, int reviewId)
        {
            var user = await _userManager.GetUserAsync(User);

            var reviews = Reviews;

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(reviews, user);
                return Page();
            }
    

            return RedirectToPage();
        }
    }
}
