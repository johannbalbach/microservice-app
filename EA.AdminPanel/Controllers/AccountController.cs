using EA.AdminPanel.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.JWT;

namespace EA.AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AccountController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
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

            var token = await _authService.Login(credentials);
            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(credentials);
            }

            Response.Cookies.Append("AccessToken", token.AccessToken, new CookieOptions { HttpOnly = true, Secure = true });

            return RedirectToAction("Index", "Home");
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
