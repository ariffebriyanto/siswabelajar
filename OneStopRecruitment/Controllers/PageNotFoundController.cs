using Microsoft.AspNetCore.Mvc;

namespace OneStopRecruitment.Controllers
{
    public class PageNotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}