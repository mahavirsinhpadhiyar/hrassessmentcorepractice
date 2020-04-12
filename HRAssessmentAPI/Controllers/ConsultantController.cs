using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Repositories.ConsultantRepository;
using Shared.ViewModels.Consultant;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HRAssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantController : ControllerBase
    {
        private readonly IConsultantRepository consultantRepository;

        public ConsultantController(IConsultantRepository consultantRepository)
        {
            this.consultantRepository = consultantRepository;
        }
        [HttpGet]
        [Route("ConsultantList")]
        public async Task<IActionResult> GetAllConsultant()
        {
            try
            {
                return Ok(await consultantRepository.GetAllConsultants());
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
                var consultantDetail = await consultantRepository.GetConsultantDetail(id);
                if (consultantDetail == null)
                    return NotFound();
                return Ok(consultantDetail);
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
                    var result = await consultantRepository.SaveConsultant(consultantVM);
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
                    var result = await consultantRepository.UpdateConsultant(consultantVM);
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
                    var result = await consultantRepository.DeleteConsultant(id);
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