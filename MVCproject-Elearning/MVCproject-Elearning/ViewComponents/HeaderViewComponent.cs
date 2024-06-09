using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;
using Newtonsoft.Json;

namespace MVCproject_Elearning.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly UserManager<AppUser> _userManager;
       
        public HeaderViewComponent(ISettingService settingService, IHttpContextAccessor accessor,
                                              UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            AppUser user = new();
            if (User.Identity.Name is not null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);

            }

            Dictionary<string, string> settings = await _settingService.GetAllAsync();

            HeaderVM response = new()
            {
                Settings = settings,
                User=user,
            };
            return await Task.FromResult(View(response));
        }

    }
    public class HeaderVM
    {
        public AppUser User { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}

