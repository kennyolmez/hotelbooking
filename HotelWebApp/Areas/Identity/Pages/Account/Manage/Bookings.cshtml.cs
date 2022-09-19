using ApplicationServices.DTOs;
using ApplicationServices.Services;
using Domain.Entities;
using HotelAppLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelWebApp.Areas.Identity.Pages.Account.Manage
{
    public class BookingsModel : PageModel
    {
        private readonly HotelServices sql;

        public BookingsModel(HotelServices _sql)
        {
            sql = _sql;
        }

        [BindProperty]
        public List<BookingsDTO> Bookings { get; set; } = new List<BookingsDTO>();

        public async Task<IActionResult> OnGetAsync()
        {
            Bookings = (await sql.GetBookingsByUserEmail(User.Identity.Name))
                .ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost(int bookingId)
        {
            await sql.UnbookRoom(bookingId);
            return RedirectToPage();
        }
    }
}
