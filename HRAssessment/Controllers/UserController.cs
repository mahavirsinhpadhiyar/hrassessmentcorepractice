using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
using Shared.Repositories.UserRepository;
using Shared.ViewModels;

namespace HRAssessment.Controllers
{
    public class UserController : Controller
    {
        //It is a good practice to create readonly if we do not want to change it's initialization after constructor
        public readonly IUserRepository _userRepository;
        //Constructor injection but not able to run without adding at Startup
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult GetUserDetail(Guid id)
        {
            UserVM userModel = new UserVM();
            userModel = _userRepository.GetUserDetail(id);

            if (userModel == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound", id);
            }

            return View(userModel);
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View(new UserVM());
        }

        [HttpPost]
        public IActionResult Registration(UserVM userModel)
        {
            if (ModelState.IsValid)
            {
                _userRepository.SaveUser(userModel);
                return RedirectToAction("Login");
            }

            return View(userModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(loginVM);
        }

        public IActionResult LogOut()
        {
            return RedirectToAction("LoginJson");
        }

        public IActionResult LoginJson()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginJson(LoginVM loginVM)
        {
            return Json(true);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserVM userModel)
        {
            if (ModelState.IsValid)
            {
                _userRepository.SaveUser(userModel);
                return RedirectToAction("Login");
            }

            return View(userModel);
        }
    }
}