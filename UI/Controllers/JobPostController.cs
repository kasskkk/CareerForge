using Application.JobPost;
using Domain.Constant;
using Domain.JobPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI.ViewModels;

namespace UI.Controllers
{
    [Authorize]
    public class JobPostController : Controller
    {
        private readonly IJobPostService _jobPostService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJobCategoryRepository _jobCategoryRepository;
        public JobPostController(IJobPostService jobPostService, UserManager<IdentityUser> userManager, IJobCategoryRepository jobCategoryRepository)
        {
            _jobPostService = jobPostService;
            _userManager = userManager;
            _jobCategoryRepository = jobCategoryRepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var approvedJobPosts = await _jobPostService.GetAllApproved();

            return View(approvedJobPosts);
        }

        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public async Task<IActionResult> Create()
        {
            var categories = await _jobCategoryRepository.GetAllAsync();

            var viewModel = new JobPostViewModel()
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public async Task<IActionResult> Create(JobPostViewModel jobPostVm)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _jobCategoryRepository.GetAllAsync();
                jobPostVm.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(jobPostVm);
            }

            var jobPost = new JobPost
            {
                Title = jobPostVm.Title,
                Company = jobPostVm.Company,
                Description = jobPostVm.Description,
                Salary = jobPostVm.Salary,
                Location = jobPostVm.Location,
                UserId = _userManager.GetUserId(User),
                JobCategoryId = jobPostVm.JobCategoryId
            };

            await _jobPostService.AddAsync(jobPost);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobPostService.DeleteAsync(id, User);

            return Ok();
        }

        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public async Task<IActionResult> MyOffers()
        {
            var myJobPosts = await _jobPostService.GetMyPosts(User);

            if (myJobPosts == null)
            {
                return NotFound();
            }

            return View(myJobPosts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var job = await _jobPostService.GetByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        [Authorize(Roles = $"{Role.ADMIN}")]
        public async Task<IActionResult> Approve()
        {
            var unapprovedPosts = await _jobPostService.GetAllUnapproved();

            if (unapprovedPosts == null)
            {
                return NotFound();
            }

            return View(unapprovedPosts);
        }

        [Authorize(Roles = $"{Role.ADMIN}")]
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            await _jobPostService.ApproveAsync(id);

            return RedirectToAction(nameof(Approve));
        }


    }
}
