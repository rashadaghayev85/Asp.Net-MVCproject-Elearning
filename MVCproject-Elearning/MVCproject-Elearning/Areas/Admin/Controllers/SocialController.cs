using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Helpers.Extensions;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
    [Area("admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class SocialController : Controller
    {

        private readonly ISocialService _socialService;
        private readonly IWebHostEnvironment _env;
        public SocialController(ISocialService socialService,
                                  IWebHostEnvironment env)
        {
            _socialService = socialService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _socialService.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Social request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

           


            bool existSocial = await _socialService.ExistAsync(request.Name);
            if (existSocial)
            {
                ModelState.AddModelError("Name", "This Social already exist");
                return View();
            }
            await _socialService.CreateAsync(new Social { Name=request.Name });
            return RedirectToAction(nameof(Index));
        }
      
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var social = await _socialService.GetByIdAsync((int)id);
            if (social == null) return NotFound();
            //return View();
            return View(new Social { Name= social.Name });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Social request)
        {
            if (id == null) return BadRequest();
            var social = await _socialService.GetByIdAsync((int)id);
            if (social == null) return NotFound();
            

            if (!ModelState.IsValid)
            {
                return View();
            }

           
            if (request.Name is not null)
            {
                social.Name = request.Name;
            }
            

            await _socialService.EditAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
