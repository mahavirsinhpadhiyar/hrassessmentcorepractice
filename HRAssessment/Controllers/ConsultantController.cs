using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Repositories.ConsultantRepository;
using Shared.ViewModels.Consultant;
using System;
using System.Threading.Tasks;

namespace HRAssessment.Controllers
{
    public class ConsultantController : Controller
    {
        public readonly IConsultantRepository _consultantRepository;
        private readonly ILogger<ConsultantController> logger;

        public ConsultantController(IConsultantRepository consultantRepository, ILogger<ConsultantController> logger)
        {
            _consultantRepository = consultantRepository;
            this.logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var consultantSet = await _consultantRepository.GetAllConsultants();

            return View(consultantSet);
        }

        [HttpGet]
        public async Task<IActionResult> InsertUpdateConsultant(Guid? Id)
        {
            //throw new Exception("Error in Details View");

            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");

            ConsultantVM consultantVM = new ConsultantVM();
            if (Id.HasValue && Id != Guid.Empty)
            {
                consultantVM = await _consultantRepository.GetConsultantDetail(new Guid(Id.ToString()));

                if (consultantVM == null)
                {
                    return View("Index");
                }
            }

            return View(consultantVM);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUpdateConsultant(ConsultantVM consultantModel)
        {
            if (ModelState.IsValid)
            {
                if (consultantModel.Id == Guid.Empty)
                {
                    if (await _consultantRepository.SaveConsultant(consultantModel))
                        TempData["ConsultantActionResponse"] = true;
                    else
                        TempData["ConsultantActionResponse"] = false;
                }
                else
                {
                    if (await _consultantRepository.UpdateConsultant(consultantModel))
                        TempData["ConsultantActionResponse"] = true;
                    else
                        TempData["ConsultantActionResponse"] = false;
                }
            }
            else
            {
                return View(consultantModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteConsultant(Guid Id)
        {
            await _consultantRepository.DeleteConsultant(Id);
            return RedirectToAction("Index");
        }
    }
}