using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;
using Newtonsoft.Json;

namespace MVCproject_Elearning.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly ISettingService _settingService;
        public HeaderViewComponent(ISettingService settingService, IHttpContextAccessor accessor)
        {
            _settingService = settingService;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

           

            Dictionary<string, string> settings = await _settingService.GetAllAsync();

            HeaderVM response = new()
            {
                Settings = settings,
            };
            return await Task.FromResult(View(response));
        }

    }
    public class HeaderVM
    {
       
        public Dictionary<string, string> Settings { get; set; }
    }
}

