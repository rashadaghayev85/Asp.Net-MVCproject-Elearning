using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels;
using System.Diagnostics;

namespace MVCproject_Elearning.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IInformationService _informationService;
        private readonly IAboutService _aboutService;
        private readonly ICategoryService _categoryService;
        public HomeController(IInformationService informationService,
                              IAboutService aboutService,
                              ICategoryService categoryService)
        {
            _informationService = informationService;
            _aboutService = aboutService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var a = await _categoryService.GetAllAsync();
            HomeVM model = new()
            {
                Informations = await _informationService.GetAllAsync(),
                Abouts= await _aboutService.GetAllAsync(),
                CategoryFirst=a.FirstOrDefault(),
                Categories=a
                //Course=
                //Instructor=
                //Slider=
                //Student=

                //Blogs = await _blogService.GetAllAsync(3),
                //Experts = await _expertService.GetAllAsync(),
                //Categories = await _categoryService.GetAllAsync(),
                //Products = await _productService.GetAllAsync(),
            };

           

            return View(model);
        }
    }
}
