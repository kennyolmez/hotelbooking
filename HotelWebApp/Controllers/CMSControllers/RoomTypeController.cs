using ApplicationServices.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers.CMSControllers
{
    public class RoomTypeController : Controller
    {
        private readonly AdminServices sqlCrud;
        private readonly HotelServices sql;
        private readonly IValidator<CMSRoomTypeCreateViewModel> createValidator;
        private readonly IValidator<CMSRoomTypeEditViewModel> editValidator;

        public RoomTypeController(AdminServices _sqlCrud, HotelServices _sql, 
            IValidator<CMSRoomTypeEditViewModel> _editValidator,
            IValidator<CMSRoomTypeCreateViewModel> _createValidator)
        {
            sqlCrud = _sqlCrud;
            sql = _sql;
            createValidator = _createValidator;
            editValidator = _editValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            CMSRoomTypeCreateViewModel viewModel = new CMSRoomTypeCreateViewModel
            {
                Amenities = (await sql.GetAmenities()).Select(x => x.Name).ToList() // Can I do queries here?
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CMSRoomTypeCreateViewModel viewModel)
        {
            var result = createValidator.Validate(viewModel);
            result.AddToModelState(this.ModelState);

            if (ModelState.IsValid)
            {
                await sqlCrud.CreateRoomType(viewModel.Title, viewModel.Price, 
                    viewModel.Description, viewModel.ImgUrl, viewModel.Size, 
                    viewModel.Amenities);

                return RedirectToAction("Index", "Admin");
            }
            
            // Otherwise I get uninstantiated object error
            CMSRoomTypeCreateViewModel output = new CMSRoomTypeCreateViewModel
            {
                Amenities = (await sql.GetAmenities()).Select(x => x.Name).ToList() 
            };

            return View("Create", output);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete()
        {
            CMSRoomTypeCreateViewModel viewModel = new CMSRoomTypeCreateViewModel
            {
                RoomTypes = await sql.GetAllRoomTypes()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await sqlCrud.DeleteRoomType(id);

            return RedirectToAction("Delete");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit()
        {
            CMSRoomTypeEditViewModel viewModel = new CMSRoomTypeEditViewModel
            {
                RoomTypes = await sql.GetAllRoomTypes(),
                Amenities = (await sql.GetAmenities()).Select(x => x.Name).ToList()
            };

            return View(viewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CMSRoomTypeEditViewModel obj)
        {
            // Validate to make sure no null values
            for (int i = 0; i < obj.Ids.Count(); i++)
            {
                if (obj.Descriptions[i] == null)
                {
                    ModelState.AddModelError("obj.Descriptions", "Descriptions field must not be blank!");
                }
                if (obj.Titles[i] == null)
                {
                    ModelState.AddModelError("obj.Titles", "Titles field must not be blank!");
                }
                if (obj.Prices[i] == null)
                {
                    ModelState.AddModelError("obj.Prices", "Prices field must not be blank!");
                }
                if (obj.Sizes[i] == null)
                {
                    ModelState.AddModelError("obj.Sizes", "Sizes field must not be blank!");
                }
                if (obj.ImgUrls[i] == null)
                {
                    ModelState.AddModelError("obj.ImgUrls", "ImgUrls field must not be blank!");
                }
            }

            if(ModelState.IsValid)
            {
                for (int i = 0; i < obj.Ids.Count(); i++)
                {
                    await sqlCrud.EditRoomType(obj.Titles[i], obj.Prices[i], obj.Descriptions[i], obj.ImgUrls[i], obj.Sizes[i], obj.Amenities, obj.Ids[i]);
                }
                    
            }
            

            return RedirectToAction("Edit");
        }

    }
}
