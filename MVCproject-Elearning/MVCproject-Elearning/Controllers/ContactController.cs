using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels;

namespace MVCproject_Elearning.Controllers
{
   
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(IContactService contactService, IHttpContextAccessor accessor,
                                              UserManager<AppUser> userManager)
        {
            _contactService = contactService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(Contact contact)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}



            await _contactService.CreateAsync(new Contact { Message = contact.Message, UserName = contact.UserName, Email = contact.Email });
            return RedirectToAction(nameof(Index));
        }
    }
}
