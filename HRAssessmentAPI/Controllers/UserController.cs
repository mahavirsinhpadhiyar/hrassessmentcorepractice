using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRAssessmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Repositories.UserRepository;
using Shared.ViewModels;

namespace HRAssessmentAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {  
            _userService = userService;
        }

        [EnableCors("CorsPolicy")]
        [HttpPost]

        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var userLoginResult = await _userService.Authenticate(loginVM);

            if (userLoginResult == null)
            {
                return BadRequest();
            }
            return Ok(userLoginResult);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUserDetails(Guid Id)
        //{
        //    var userDetails = await _userService.Authenticate
        //}
    }
}