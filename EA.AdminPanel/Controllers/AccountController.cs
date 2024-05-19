using EA.AdminPanel.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.JWT;
using System.Web.Http.ModelBinding;

namespace EA.AdminPanel.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCredentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(credentials);
            }

            var token = await _userService.Login(credentials);
            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(credentials);
            }

            // Отправляем токен на клиентскую сторону для сохранения в LocalStorage
            return Json(new { success = true, accessToken = token.AccessToken, refreshToken = token.RefreshToken });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Profile()
        {
            var profile = await _userService.GetProfileAsync();
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string password)
        {
            if (ModelState.IsValid)
            {
                await _userService.ChangePasswordAsync(password);
                return RedirectToAction("Profile");
            }
            return View(password);
        }
    }
}
