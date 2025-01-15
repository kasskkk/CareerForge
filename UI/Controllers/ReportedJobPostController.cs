using Application.ReportedPostService;
using Domain.Enum;
using Domain.JobPost;
using Domain.ReportedPost;
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

        [HttpPost]
        public async Task<IActionResult> Report(int id, ReportName reportName, string description)
        {

            var report = new ReportedJobPost()
            {
                JobPostId = id,
                Name = reportName, 
                Description = description
            };

            await _service.AddAsync(report);
            return RedirectToAction(nameof(JobPostController.Details), nameof(JobPost), new { id = id });
        }
    }
}
