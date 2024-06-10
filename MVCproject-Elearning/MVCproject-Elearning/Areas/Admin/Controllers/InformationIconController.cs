using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Helpers.Extensions;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.InformationIcon;
using MVCproject_Elearning.ViewModels.Informations;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
    [Area("admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class InformationIconController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly IIconService _iconService;
        private readonly IInformationService _informationService;
        public InformationIconController(IIconService iconService, IWebHostEnvironment env,
                                    IInformationService informationService)
        {
            _iconService = iconService;
            _env = env;
            _informationService = informationService;
        }

        public async Task<IActionResult> Index()
        {
            var icon = await _iconService.GetAllAsync();

            List<InformationIconVM> icons = icon.Select(m => new InformationIconVM { Id = m.Id, Name = m.Icon}).ToList();

            return View(icons);
        }
        [HttpGet]
        public async Task<IActionResult> IconCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IconCreate(InformationIconCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            InformationIcon icon = new()
            {
                Icon = request.Name


            };

            await _iconService.CreateAsync(icon);


            return RedirectToAction(nameof(Index));


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var deleteIcon = await _iconService.GetByIdAsync((int)id);
            if (deleteIcon == null) return NotFound();
            

            _iconService.DeleteAsync(deleteIcon);

            return RedirectToAction(nameof(Index));
        }
    }
}
