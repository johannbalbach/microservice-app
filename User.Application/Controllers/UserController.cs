using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Shared.Models;
using Shared.Interfaces;
using Shared.DTO;
using Shared.Exceptions;
using System.Security.Cryptography;
using System.Text;
using Shared.Validators;

namespace User.Application.Controllers
{
    [ApiController]
    [Route("api/user/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response>> ChangePassword([FromQuery][Required][PasswordValidation] string password)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _userService.ChangePassword(password, userEmailClaim);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Response>> Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"];

            return await _userService.LogoutUser(token.ToString().Substring(7));
        }


        [HttpPost]
        public async Task<ActionResult<Response>> Login([FromBody] LoginCredentials body)
        {
            body.Password = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(body.Password)));

            return await _userService.LoginUser(body);
        }

        [Authorize(Policy = "ApplicantOrManager")]
        [HttpGet]
        public async Task<ActionResult<UserProfileDTO>> GetProfile()
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _userService.UserProfileGet(userEmailClaim);
        }

        [Authorize(Policy = "Manager")]
        [HttpPut]
        public async Task<ActionResult<UserProfileDTO>> PutProfile([FromBody] UserProfileEditDTO body)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _userService.UserProfilePut(body, userEmailClaim);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> RegisterApplicant([FromBody] ApplicantRegisterDTO body)
        {
            return await _userService.UserRegisterApplicantPost(body);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> RegisterUser([FromBody] UserRegisterDTO body)
        {
            return await _userService.UserRegister(body);
        }

    }
}
