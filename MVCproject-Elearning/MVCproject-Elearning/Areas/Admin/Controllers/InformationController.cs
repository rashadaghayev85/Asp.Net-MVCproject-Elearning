using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Helpers.Extensions;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.Informations;
using MVCproject_Elearning.ViewModels.Sliders;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
	[Area("admin")]
	public class InformationController : Controller
	{
        private readonly IWebHostEnvironment _env;
        private readonly IIconService _iconService;
        private readonly IInformationService _informationService;
        public InformationController(IIconService iconService, IWebHostEnvironment env,
                                    IInformationService informationService)
        {
            _iconService = iconService;
            _env = env;
            _informationService = informationService;
        }
        public async Task<IActionResult> Index()
		{
            var information = await _informationService.GetAllAsync();

            List<InformationVM> informations = information.Select(m => new InformationVM { Id = m.Id, Icon = m.InformationIcon.Icon, Title = m.Title, Description = m.Description }).ToList();

            return View(informations);
		}
        [HttpGet]
        public async Task<IActionResult> Create()
        {
              var a= await _iconService.GetAllSelectedAsync();
            ViewBag.icons = a;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InformationCreateVM request)
        {
            ViewBag.icons = await _iconService.GetAllSelectedAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }


            Information ınformation = new()
            {
                Title = request.Title,
                Description = request.Description,

                InformationIconId =request.IconId,
              

            };

            await _informationService.CreateAsync(ınformation);


            return RedirectToAction(nameof(Index));


        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var information = await _informationService.GetByIdWithIconAsync((int)id);
            if (information is null) return NotFound();


            InformationDetailVM response = new()
            {
                
                Title = information.Title,
                Description = information.Description,
                Icon = information.InformationIcon.Icon,
                
            };
            return View(response);
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return BadRequest();
			var deleteInformation = await _informationService.GetByIdAsync((int)id);
			if (deleteInformation == null) return NotFound();


            _informationService.DeleteAsync(deleteInformation);


            return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{


			if (id is null) return BadRequest();

			var existInformation = await _informationService.GetByIdWithIconAsync((int)id);

			if (existInformation is null) return NotFound();



			ViewBag.icons = await _iconService.GetAllSelectedAsync();

			

			InformationEditVM response = new()
			{
				Title = existInformation.Title,
				Description = existInformation.Description,
				IconId = existInformation.InformationIconId,
			};





			return View(response);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, InformationEditVM request)
		{
			ViewBag.icons = await _iconService.GetAllSelectedAsync();
			if (!ModelState.IsValid)
			{
				//var product = await _informationService.GetByIdAsync((int)id);

				

				return View();

			}

			if (id == null) return BadRequest();
			var information = await _informationService.GetByIdWithIconAsync((int)id);
			if (information== null) return NotFound();


			

			if (request.Title is not null)
			{
				information.Title = request.Title;
			}
			if (request.Description is not null)
			{
				information.Description = request.Description;
			}
			if (request.IconId != 0)
			{
				information.InformationIconId = request.IconId;
			}
			







			await _informationService.EditAsync();
			return RedirectToAction(nameof(Index));
		}

	}
}
