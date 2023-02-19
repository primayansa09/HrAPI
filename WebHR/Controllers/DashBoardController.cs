using Microsoft.AspNetCore.Mvc;

namespace WebHR.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Role()
        {
            return View();
        }
        public IActionResult Departement()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
        }
    }
}
