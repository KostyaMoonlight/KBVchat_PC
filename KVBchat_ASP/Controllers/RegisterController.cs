using BusinessLogic.DTO.User;
using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KVBchat_ASP.Controllers
{
    public class RegisterController 
        : Controller
    {
        IUserService _userService = null;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegistrationViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (_userService.RegisterUser(user))
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            return View(user);
        }
    }
}