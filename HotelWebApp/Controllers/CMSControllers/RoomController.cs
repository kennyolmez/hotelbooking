using ApplicationServices.Services;
using HotelWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers.CMSControllers
{
    public class RoomController : Controller
    {
        private readonly HotelServices sql;
        private readonly AdminServices sqlCrud;
        public RoomController(HotelServices _sql, AdminServices _sqlCrud)
        {
            sql = _sql;
            sqlCrud = _sqlCrud;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            // Get All room numbers to exlcude async
            var roomsToExclude = (await sql.GetAllRooms()).Select(x => x.RoomNumber).ToList(); 

            CMSRoomViewModel viewModel = new CMSRoomViewModel()
            {
                RoomTypes = (await sql.GetAllRoomTypes()).Select(x => x.Title).ToList(),
                //AvailableRoomNumbers between 1-100
                RoomNumbers = Enumerable.Range(1, 100).Except(roomsToExclude).ToList()
            };

            return View(viewModel);
        }
        // Refactor this controller, using fluent validation

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CMSRoomViewModel viewModel) 
        {
            var roomsToExclude = (await sql.GetAllRooms()).Select(x => x.RoomNumber).ToList();

            // Makes sure they cannot input duplicates via the inspector
            if (roomsToExclude.Contains(viewModel.RoomNumber))
            {
                ModelState.AddModelError("", "Room Number is not available");
            }
            // If the room type selected does not exist 
            if (!(await sql.GetAllRoomTypes()).Select(x => x.Title).Contains(viewModel.RoomType)) 
            {
                ModelState.AddModelError("", "Room type is not valid");
            }

            if(ModelState.IsValid)
            {
                await sqlCrud.CreateRoom(viewModel.RoomType, viewModel.RoomNumber);

                return RedirectToAction("Index", "Admin");
            }


            CMSRoomViewModel output = new CMSRoomViewModel()
            {
                RoomTypes = (await sql.GetAllRoomTypes()).Select(x => x.Title).ToList(),
                RoomNumbers = Enumerable.Range(1, 100).Except(roomsToExclude).ToList()
            };

            return View("Create", output);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete()
        {
            CMSRoomViewModel viewModel = new CMSRoomViewModel
            {
                RoomNumbers = (await sql.GetAllRooms()).Select(x => x.RoomNumber).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int roomNumber)
        {
            await sqlCrud.DeleteRoom(roomNumber);

            return RedirectToAction("Delete");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit()
        {
            CMSRoomViewModel viewModel = new CMSRoomViewModel
            {
                RoomNumbers = (await sql.GetAllRooms()).Select(x => x.RoomNumber).ToList(),
                RoomTypes = (await sql.GetAllRoomTypes()).Select(x => x.Title).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            return RedirectToAction("Delete");
        }

    }
}
