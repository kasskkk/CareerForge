using Application.ReportedPostService;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class ReportedJobPostController : Controller
    {
        private readonly IReportedJobPostService _service;
        public ReportedJobPostController(IReportedJobPostService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var reportedPosts = await _service.GetAllAsync();

            return View(reportedPosts);
        }
    }
}
