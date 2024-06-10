using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;

namespace MVCproject_Elearning.ViewComponents
{
    public class ContactViewComponent:ViewComponent
    {
        private readonly ISettingService _settingService;
        public ContactViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = new ContactVMVC
            {
                Settings = await _settingService.GetAllAsync(),
            };

            return View(datas);
        }


    }
    public class ContactVMVC
    {
       
        public IDictionary<string, string> Settings { get; set; }
    }
}
