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
        private readonly ICourseService _courseService;
        private readonly IInstructorService _instructorService;
        private readonly IStudentService _studentService;

        public HomeController(IInformationService informationService,
                              IAboutService aboutService,
                              ICategoryService categoryService,
                              ICourseService courseService,
                              IInstructorService instructorService,
                              IStudentService studentService)
        {
            _informationService = informationService;
            _aboutService = aboutService;
            _categoryService = categoryService;
            _courseService = courseService;
            _instructorService = instructorService;
            _studentService = studentService;
        }
        public async Task<IActionResult> Index()
        {
            var a = await _categoryService.GetAllAsync();
            HomeVM model = new()
            {
                Informations = await _informationService.GetAllAsync(),
                Abouts = await _aboutService.GetAllAsync(),
                CategoryFirst = a.FirstOrDefault(),
                CategoryLast = a.LastOrDefault(),
                Categories = a.Skip(1).Take(2),
                Courses= await _courseService.GetAllAsync(),    
                Instructors=await _instructorService.GetAllAsync(), 
                Students=await _studentService.GetAllAsync(),   
                
            };

           

            return View(model);
        }
    }
}
