using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewComponents;
using MVCproject_Elearning.ViewModels;

namespace MVCproject_Elearning.Controllers
{
   
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISettingService _settingService;

        public ContactController(IContactService contactService, IHttpContextAccessor accessor,
                                              UserManager<AppUser> userManager, ISettingService settingService)
        {
            _contactService = contactService;
            _userManager = userManager;
            _settingService = settingService;
        }
        public async Task<IActionResult> Index()
        {

            AppUser user = new();
            if (User.Identity.Name is not null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);

            }
            ContactVM model = new()
            {
                UserFullName = user.FullName,
                Email= user.Email,
            };
            return View(model); 


           
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(ContactVM contact)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}



            await _contactService.CreateAsync(new Contact { Message = contact.Message, UserName = contact.UserFullName, Email = contact.Email });
            return RedirectToAction(nameof(Index));
        }
    }
}
