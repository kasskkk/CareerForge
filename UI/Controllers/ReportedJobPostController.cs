using Application.ReportedPostService;
using Domain.Enum;
using Domain.JobPost;
using Domain.ReportedPost;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels;

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
        public async Task<IActionResult> Report(DetailsReportViewModel viewModel)
        {
            var report = new ReportedJobPost()
            {
                JobPostId = viewModel.Id,
                Name = viewModel.ReportName, 
                Description = viewModel.ReportDescription
            };

            await _service.AddAsync(report);
            return RedirectToAction(nameof(JobPostController.Details), nameof(JobPost), new { id = viewModel.Id });
        }
    }
}
