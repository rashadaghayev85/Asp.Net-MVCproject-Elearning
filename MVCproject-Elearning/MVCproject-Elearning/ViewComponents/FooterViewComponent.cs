using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;

namespace MVCproject_Elearning.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly ISocialMediaCompanyService _socialMediaService;
        private readonly ISettingService _settingService;
        public FooterViewComponent(ISocialMediaCompanyService socialMediaService, ISettingService settingService)
        {
            _socialMediaService = socialMediaService;
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = new FooterVMVC
            {
                SocialMedias = await _socialMediaService.GetAllAsync(),
                Settings = await _settingService.GetAllAsync(),
            };

            return View(datas);
        }
    }
    public class FooterVMVC
    {
        public IEnumerable<SocialMediaCompany> SocialMedias { get; set; }
        public IDictionary<string, string> Settings { get; set; }
    }
}

