using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.Categories;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
    [Area("admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IWebHostEnvironment _env;
        public ContactController(IContactService contactService,

                                   IWebHostEnvironment env)
        {
            _contactService = contactService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contactService.GetAllAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var category = await _contactService.GetByIdAsync((int)id);
            if (category is null) return NotFound();
            await _contactService.DeleteAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {


            Contact contact = await _contactService.GetByIdAsync((int)id);
            return View(contact);
        }



    }
}
