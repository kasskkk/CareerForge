using Application.JobPost;
using Domain.JobPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels;

namespace UI.Controllers
{
    public class JobPostController : Controller
    {
        private readonly IJobPostService _jobPostService;
        private readonly UserManager<IdentityUser> _userManager;
        public JobPostController(IJobPostService jobPostService, UserManager<IdentityUser> userManager)
        {
            _jobPostService = jobPostService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var jobPosts = await _jobPostService.GetAllAsync();

            return View(jobPosts);
        }

        public IActionResult Create()
        {
            return View();
        }

        //change later on viewmodel if works
        [HttpPost]
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

                Console.WriteLine("Dodawanie chyba1");
                await _jobPostService.AddAsync(jobPost);

                Console.WriteLine("Dodawanie chyba2");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("Nie dzialaaaaa");
            return View(jobPostVm);
        }

    }
}
