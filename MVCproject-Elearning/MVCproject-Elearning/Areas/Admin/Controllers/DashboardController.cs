using Microsoft.AspNetCore.Mvc;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
        [Area("admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
