using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
        [Area("admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
