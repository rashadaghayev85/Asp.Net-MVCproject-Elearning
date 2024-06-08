using Microsoft.AspNetCore.Mvc;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
    [Area("admin")]
    public class StudentController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
