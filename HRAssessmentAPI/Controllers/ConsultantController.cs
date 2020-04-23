using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
using Shared.Helpers;
using Shared.Services;
using Shared.ViewModels.Consultant;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRAssessmentAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantController : ControllerBase
    {
        private readonly IConsultantService consultantService;
        private readonly UserManager<AppUser> userManager;
        private string userId;
        public ConsultantController(IConsultantService consultantService, UserManager<AppUser> userManager)
        {
            this.consultantService = consultantService;
            this.userManager = userManager;
        }
        [HttpGet]
        [Route("ConsultantList")]
        public async Task<IActionResult> GetAllConsultant()
        {
            try
            {
                userId = User.FindFirstValue(ClaimTypes.Name);
                return Ok(await consultantService.GetAllConsultants(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message ?? ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("GetConsultant")]
        public async Task<IActionResult> GetSingleConsultant(Guid id)
        {
            try
            {
                var consultantDetail = await consultantService.GetConsultantDetail(id);
                if (consultantDetail == null)
                    return NotFound();
                return Ok(consultantDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message ?? ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("GetTotalConsultantCount")]
        public async Task<IActionResult> GetTotalConsultantCount()
        {
            try
            {
                userId = User.FindFirstValue(ClaimTypes.Name);
                return Ok(await consultantService.GetTotalConsultantCount(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message ?? ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> InsertNewConsultant(ConsultantVM consultantVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    consultantVM.UserId = User.FindFirstValue(ClaimTypes.Name);
                    var result = await consultantService.SaveConsultant(consultantVM);
                    if (result == DbStatusCode.Created)
                    {
                        return Ok(HttpStatusCode.Created);
                    }
                    else if (result == DbStatusCode.Exception)
                    {
                        return StatusCode(500);
                    }
                    else
                        return Forbid();
                    //We can also return status like below
                    //return StatusCode(403);       //Forbidden: 403
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message ?? ex.InnerException.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsultant(ConsultantVM consultantVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    consultantVM.UserId = User.FindFirstValue(ClaimTypes.Name);
                    var result = await consultantService.UpdateConsultant(consultantVM);
                    if (result == DbStatusCode.Updated)
                    {
                        return Ok();
                    }
                    else if (result == DbStatusCode.NotFound)
                    {
                        return StatusCode(404);
                    }
                    else if (result == DbStatusCode.Exception)
                    {
                        return StatusCode(500);
                    }
                    else
                        return Forbid();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message ?? ex.InnerException.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveConsultant(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await consultantService.DeleteConsultant(id);
                    if (result == DbStatusCode.Deleted)
                    {
                        return Ok();
                    }
                    else if (result == DbStatusCode.NotFound)
                    {
                        return StatusCode(404);
                    }
                    else if (result == DbStatusCode.Exception)
                    {
                        return StatusCode(500);
                    }
                    else
                        return Forbid();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message ?? ex.InnerException.Message);
            }
        }
    }
}