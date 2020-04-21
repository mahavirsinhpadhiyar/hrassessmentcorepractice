using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Helpers;
using Shared.Repositories;
using Shared.Services;
using Shared.ViewModels;

namespace HRAssessmentAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;
        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpGet]
        [Route("CompanyList")]
        public async Task<IActionResult> GetAllCompany()
        {
            try
            {
                return Ok(companyService.GetAllCompanys());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message ?? ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("GetCompany")]
        public async Task<IActionResult> GetSingleCompany(Guid id)
        {
            try
            {
                var companyDetail = await companyService.GetCompanyDetail(id);
                if (companyDetail == null)
                    return NotFound();
                return Ok(companyDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message ?? ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("GetTotalCompanyCount")]
        public async Task<IActionResult> GetTotalCompanyCount()
        {
            try
            {
                return Ok(await companyService.GetTotalCompanyCount());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message ?? ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> InsertNewCompany(CompanyVM companyVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await companyService.SaveCompany(companyVM);
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
        public async Task<IActionResult> UpdateCompany(CompanyVM companyVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await companyService.UpdateCompany(companyVM);
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
        public async Task<IActionResult> RemoveCompany(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await companyService.DeleteCompany(id);
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