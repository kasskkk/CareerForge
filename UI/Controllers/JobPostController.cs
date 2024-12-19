using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class JobPostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
