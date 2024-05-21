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
using Shared.DTO.Query;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserProfileDTO>> GetProfile()
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _userService.UserProfileGet(userEmailClaim);
        }
        [HttpPost]
        public async Task<ActionResult<Response>> UpdateToken([FromBody] string refresh_token)
        {
            return await _userService.UpdateToken(refresh_token);
        }

        [Authorize]
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

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("/management/addMainManager/{userId}")]
        public async Task<ActionResult<Response>> AddMainManager([FromRoute][Required] Guid userId)
        {
            return await _userService.AddMainManager(userId);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("/management/addManager")]
        public async Task<ActionResult<Response>> AddManager([FromBody] ManagerRegisterDTO body)
        {
            return await _userService.AddManager(body);
        }

        [HttpPut]
        [Authorize(Policy = "Privileged")]
        [Route("/management/editApplicant/{id}")]
        public async Task<ActionResult<ApplicantProfileDTO>> EditApplicantByIdManager([FromBody] ApplicantProfileEditDTO body, [FromRoute][Required] Guid id)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _userService.EditApplicantByIdManager(body, id, userEmailClaim);
        }

        [HttpGet]
        [Authorize(Policy = "Privileged")]
        [Route("/management/getApplicant/{id}")]
        public async Task<ActionResult<ApplicantProfileDTO>> GetApplicantByIdManager([FromRoute][Required] Guid id)
        {
            return await _userService.GetApplicantByIdManager(id);
        }

        [HttpGet]
        [Authorize(Policy = "Privileged")]
        [Route("/management/getListOfApplicants")]
        public async Task<ActionResult<ApplicantWithPaginationInfo>> GetApplicantsListWithFiltering([FromQuery] ApplicantsFilterQuery query)
        {
            return await _userService.GetApplicantsListWithFiltering(query);
        }

        [HttpGet]
        [Authorize(Policy = "MainManagerOrAdmin")]
        [Route("/management/getListOfManagers")]
        public async Task<ActionResult<ManagerWithPaginationInfo>> GetListOfManagersWithFiltering([FromQuery] ManagersFilterQuery query)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _userService.GetListOfManagersWithFiltering(query, userEmailClaim);
        }

        [HttpGet]
        [Authorize]
        [Route("/api/user/check-token")]
        public async Task<ActionResult<Response>> CheckToken()
        {
            var token = HttpContext.Request.Headers["Authorization"];

            return await _userService.CheckToken(token.ToString().Substring(7));
        }
    }
}
