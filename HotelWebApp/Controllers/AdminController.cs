using ApplicationServices.DTOs;
using ApplicationServices.Services;
using HotelWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly HotelServices sql;
        private readonly AdminServices sqlCrud;
        public AdminController(HotelServices _sql, AdminServices _sqlCrud)
        {
            sql = _sql;
            sqlCrud = _sqlCrud;
        }
    

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<string> itemsToEdit = new List<string>
            {
                "Amenity",
                "Room",
                "RoomType"
            };

            CMSIndexViewModel viewModel = new CMSIndexViewModel
            {
                ItemsToEdit = itemsToEdit
            };

            return View(viewModel);
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(string item)
        {
            if(item == "Amenity")
            {
                return View("~/Views/Amenity/Create.cshtml");
            }
            else if (item == "Room")
            {
                return View("AddRoom");
            }
            else if (item == "RoomType")
            {
                return RedirectToAction("AddRoomType");
            }

            return View("Index");
        }


    }
}
