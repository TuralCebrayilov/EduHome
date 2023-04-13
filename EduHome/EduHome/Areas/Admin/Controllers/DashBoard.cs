using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
