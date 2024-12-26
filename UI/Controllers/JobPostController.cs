using Application.JobPost;
using Domain.Constant;
using Domain.JobPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels;

namespace UI.Controllers
{
    [Authorize]
    public class JobPostController : Controller
    {
        private readonly IJobPostService _jobPostService;
        private readonly UserManager<IdentityUser> _userManager;
        public JobPostController(IJobPostService jobPostService, UserManager<IdentityUser> userManager)
        {
            _jobPostService = jobPostService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var jobPosts = await _jobPostService.GetAllAsync();

            return View(jobPosts);
        }

        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public async Task<IActionResult> Create(JobPostViewModel jobPostVm)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("jest valid");

                var jobPost = new JobPost()
                {
                    Title = jobPostVm.Title,
                    Company = jobPostVm.Company,
                    Description = jobPostVm.Description,
                    Salary = jobPostVm.Salary,
                    Location = jobPostVm.Location,
                    UserId = _userManager.GetUserId(User)
                };

                await _jobPostService.AddAsync(jobPost);

                return RedirectToAction(nameof(Index));
            }
            return View(jobPostVm);
        }

        [HttpDelete]
        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobPostService.DeleteAsync(id, User);

            return Ok();
        }

    }
}
