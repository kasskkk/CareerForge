using Application.User;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UI.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        private readonly UserManager<IdentityUser> _userManager;
        public UserInfoController(IUserInfoService userInfoService, UserManager<IdentityUser> userManager)
        {
            _userInfoService = userInfoService;
            _userManager = userManager;
        }
        public async Task<IActionResult> MyCV()
        {
            var userId = _userManager.GetUserId(User);

            var userInfos = await _userInfoService.GetAllAsync();

            var userInfo = userInfos.FirstOrDefault(u => u.UserId == userId);

            return View(userInfo);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCV(IFormFile cvFile)
        {
            if (cvFile == null || cvFile.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            var userId = _userManager.GetUserId(User);
            var userInfo = (await _userInfoService.GetAllAsync()).FirstOrDefault(u => u.UserId == userId);

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{cvFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await cvFile.CopyToAsync(fileStream);
            }

            var cvFilePath = $"/uploads/{uniqueFileName}";

            if (userInfo == null)
            {
                userInfo = new UserInfo { UserId = userId, CVFilePath = cvFilePath };
                await _userInfoService.AddAsync(userInfo);
            }
            else
            {
                userInfo.CVFilePath = cvFilePath;
                await _userInfoService.UpdateAsync(userInfo);
            }

            return RedirectToAction("MyCV"); // Przekierowanie do widoku MyCV
        }
    }
}
