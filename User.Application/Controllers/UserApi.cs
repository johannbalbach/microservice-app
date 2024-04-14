using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Shared.Models;
using Shared;
using Shared.Interfaces;
using Shared.DTO.UserDTO;

namespace User.Domain.Controllers
{
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserApiController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/user/changePassword")]
        public async Task<ActionResult<Response>> ChangePassword([FromQuery][Required()] string password)
        {
            return await _userService.ChangePassword(password);
        }

        [HttpGet]
        [Route("/user/logout")]
        public async Task<ActionResult<Response>> LogoutUser()
        {
            return await _userService.LogoutUser();
        }

        [HttpPost]
        [Route("/user/login")]
        public async Task<ActionResult<Response>> LoginUser([FromBody] LoginCredentials body)
        {
            return await _userService.LoginUser(body);
        }

        [HttpGet]
        [Route("/user/profile")]
        public async Task<ActionResult<UserProfileDTO>> UserProfileGet()
        {
            return await _userService.UserProfileGet();
        }

        [HttpPut]
        [Route("/user/profile")]
        public async Task<ActionResult<ApplicantProfileDTO>> UserProfilePut([FromBody] UserProfileEditDTO body, [FromQuery] string type)
        {
            return await _userService.UserProfilePut(body, type);
        }

        [HttpPost]
        [Route("/user/registerApplicant")]
        public async Task<ActionResult<Response>> UserRegisterApplicantPost([FromBody] ApplicantRegisterDTO body)
        {
            return await _userService.UserRegisterApplicantPost(body);
        }

        [HttpPost]
        [Route("/user/register")]
        public async Task<ActionResult<Response>> UserRegister([FromBody] UserRegisterDTO body)
        {
            return await _userService.UserRegister(body);
        }
    }
}
