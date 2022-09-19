using ApplicationServices.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers.CMSControllers
{
    public class AmenityController : Controller
    {
        private readonly HotelServices sql;
        private readonly AdminServices sqlCrud;
        private readonly IValidator<CMSAmenityEditViewModel> editValidator;
        private readonly IValidator<CMSAmenityCreateViewModel> createValidator;
        public AmenityController(HotelServices _sql, AdminServices _sqlCrud, 
            IValidator<CMSAmenityEditViewModel> _editValidator, 
            IValidator<CMSAmenityCreateViewModel> _createValidator)
        {
            sql = _sql;
            sqlCrud = _sqlCrud;
            editValidator = _editValidator;
            createValidator = _createValidator;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CMSAmenityCreateViewModel obj)
        {
            var result = createValidator.Validate(obj);
            result.AddToModelState(this.ModelState);


            if (ModelState.IsValid)
            {
                await sqlCrud.CreateAmenity(obj.Name, obj.Icon);

                return RedirectToAction("Index", "Admin");
            }

            return View(obj);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit()
        {
            CMSAmenityEditViewModel viewModel = new CMSAmenityEditViewModel
            {
                Amenity = await sql.GetAmenities(),
            };

            return View(viewModel);
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CMSAmenityEditViewModel obj)
        {
            var result = editValidator.Validate(obj);
            result.AddToModelState(this.ModelState);

            if (ModelState.IsValid)
            {
                for (int i = 0; i < obj.Ids.Count(); i++)
                {
                    await sqlCrud.EditAmenity(obj.Names[i], obj.Icons[i], obj.Ids[i]);
                }

                return RedirectToAction("Index", "Admin");
            }

            CMSAmenityEditViewModel viewModel = new CMSAmenityEditViewModel
            {
                Amenity = await sql.GetAmenities(),
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete()
        {
            CMSAmenityCreateViewModel viewModel = new CMSAmenityCreateViewModel
            {
                Amenity = await sql.GetAmenities()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await sqlCrud.DeleteAmenity(id);

            return RedirectToAction("Delete");
        }
    }
}
