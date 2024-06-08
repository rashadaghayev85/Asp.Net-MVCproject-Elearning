using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.ViewModels;
using System.Diagnostics;

namespace MVCproject_Elearning.Controllers
{
    public class HomeController : Controller
    {
       

        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {

                //Blogs = await _blogService.GetAllAsync(3),
                //Experts = await _expertService.GetAllAsync(),
                //Categories = await _categoryService.GetAllAsync(),
                //Products = await _productService.GetAllAsync(),
            };

           

            return View(model);
        }
    }
}
