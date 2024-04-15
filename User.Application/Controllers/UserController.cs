using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Shared.Models;
using Shared;
using Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Shared.DTO;

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

        [Authorize(Policy = "Applicant")]
        [HttpPost]
        public async Task<ActionResult<Response>> ChangePassword([FromQuery][Required()] string password)
        {
            return await _userService.ChangePassword(password);
        }

        [Authorize(Policy = "Manager")]
        [HttpGet]
        public async Task<ActionResult<Response>> Logout()
        {
            return await _userService.LogoutUser();
        }


        [HttpPost]
        public async Task<ActionResult<Response>> Login([FromBody] LoginCredentials body)
        {
            return await _userService.LoginUser(body);
        }

        [Authorize(Policy = "applicant")]
        [HttpGet]
        public async Task<ActionResult<UserProfileDTO>> GetProfile()
        {
            return await _userService.UserProfileGet();
        }

        [Authorize(Policy = "manager")]
        [HttpPut]
        public async Task<ActionResult<ApplicantProfileDTO>> PutProfile([FromBody] UserProfileEditDTO body, [FromQuery] string type)
        {
            return await _userService.UserProfilePut(body, type);
        }

        [Authorize]
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
