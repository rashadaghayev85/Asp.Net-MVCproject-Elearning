using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCproject_Elearning.Helpers.Extensions;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.Abouts;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class SettingController : Controller
	{
		private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public  async Task<IActionResult> Index()
		{
			Dictionary<string,string> datas = await _settingService.GetAllAsync();
           
            return View(datas);
		}
       


        [HttpGet]
        public async Task<IActionResult> Edit(string key)
        {
            if (key == null) return BadRequest();

            var about = await _settingService.GetByKeyAsync(key);
            if (about == null) return NotFound();

            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string key, Setting request)
        {
            if (key == null) return BadRequest();
            var about = await _settingService.GetByKeyAsync(key);
            if (about == null) return NotFound();
           

            if (!ModelState.IsValid)
            {
                return View();
            }

           
           
            if (request.Value is not null)
            {
                about["Value"] = request.Value;
            }

            await _settingService.EditAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
