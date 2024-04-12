using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using User.Domain.Models;

namespace User.Domain.Controllers
{
    [ApiController]
    public class UserApiController : ControllerBase
    {
        [HttpPost]
        [Route("/user/changePassword")]

        public async Task<ActionResult<Response>> ChangePassword([FromQuery][Required()] string password)
        {
        }

        [HttpGet]
        [Route("/user/logout")]
        public async Task<ActionResult<Response>> LogoutUser()
        {
        }

        [HttpPost]
        [Route("/user/login")]
        public async Task<ActionResult<Response>> LoginUser([FromBody] LoginCredentials body)
        {
        }

        [HttpGet]
        [Route("/user/profile")]
        public async Task<ActionResult<UserProfileDTO>> UserProfileGet()
        {

        }

        [HttpPut]
        [Route("/user/profile")]
        public async Task<ActionResult<ApplicantProfileDTO>> UserProfilePut([FromBody] UserProfileEditDTO body, [FromQuery] string type)
        {

        }

        [HttpPost]
        [Route("/user/registerApplicant")]
        public async Task<ActionResult<Response>> UserRegisterApplicantPost([FromBody] ApplicantRegisterDTO body)
        {

        }

        [HttpPost]
        [Route("/user/register")]

        public async Task<ActionResult<Response>> UserRegister([FromBody] UserRegisterDTO body)
        {
        }
    }
}
