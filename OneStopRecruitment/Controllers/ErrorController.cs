using Microsoft.AspNetCore.Mvc;

namespace OneStopRecruitment.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
