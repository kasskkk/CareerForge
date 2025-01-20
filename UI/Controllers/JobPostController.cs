using Application.JobApplication;
using Application.JobPost;
using Application.User;
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
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IUserInfoService _userInfoService;
        public JobPostController(IJobPostService jobPostService, UserManager<IdentityUser> userManager, IJobCategoryRepository jobCategoryRepository, IJobApplicationService jobApplicationService, IUserInfoService userInfoService)
        {
            _jobPostService = jobPostService;
            _userManager = userManager;
            _jobCategoryRepository = jobCategoryRepository;
            _jobApplicationService = jobApplicationService;
            _userInfoService = userInfoService;
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

        [HttpPost]
        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _jobPostService.GetByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if (User.IsInRole(Role.ADMIN) && job.UserId != userId)
            {
                return Unauthorized("Unauthorized action");
            }

            await _jobPostService.DeleteAsync(id, User);

            if (User.IsInRole(Role.ADMIN))
            {
                return RedirectToAction(nameof(Approve));
            }
            else
            {
                return RedirectToAction(nameof(MyOffers));
            }
        }

        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public async Task<IActionResult> MyOffers()
        {
            var myJobPosts = await _jobPostService.GetMyPosts(User);

            if (myJobPosts == null)
            {
                return NotFound();
            }

            foreach (var job in myJobPosts)
            {
                job.JobApplications = (await _jobApplicationService.GetJobApplicationsByJobPostIdAsync(job.Id)).ToList();
            }

            var sortedJobPosts = myJobPosts.OrderByDescending(job => job.IsApproved).ToList();

            return View(sortedJobPosts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, bool fromApprove)
        {
            var job = await _jobPostService.GetByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if (!job.IsApproved && !User.IsInRole(Role.ADMIN) && job.UserId != userId)
            {
                return Unauthorized();
            }

            var details = new DetailsReportViewModel()
            {
                Id = job.Id,
                Title = job.Title,
                Company = job.Company,
                Location = job.Location,
                Salary = job.Salary,
                Description = job.Description,
                Created = job.Created,
                JobCategoryId = job.JobCategoryId,
                Category = job.Category
            };

            ViewBag.FromApprove = fromApprove;
            return View(details);
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

        [Authorize(Roles = $"{Role.JOBSEEKER}")]
        [HttpPost]
        public async Task<IActionResult> Apply(int id)
        {
            var userId = _userManager.GetUserId(User);

            var userInfo = await _userInfoService.GetByUserIdAsync(userId);

            var jobApplication = new JobApplication()
            {
                JobPostId = id,
                UserInfoId = userInfo.Id,
            };
            await _jobApplicationService.AddAsync(jobApplication);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = $"{Role.ADMIN},{Role.EMPLOYER}")]
        public async Task<IActionResult> JobApplication(int id)
        {
            var jobApps = await _jobApplicationService.GetJobApplicationsByJobPostIdAsync(id);

            if (jobApps == null)
            {
                return NotFound();
            }

            foreach (var jobApp in jobApps)
            {
                jobApp.UserInfo = await _userInfoService.GetByIdAsync(jobApp.UserInfoId);
            }

            return View(jobApps);
        }
    }
}
