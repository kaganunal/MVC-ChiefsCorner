using Microsoft.AspNetCore.Mvc;

namespace MVC_ChiefsCorner.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
