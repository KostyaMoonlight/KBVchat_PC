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
    public class LoginController : Controller
    {
        IAuthenticationService _authenticationService = null;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (_authenticationService.Authenticate(viewModel))
            {
                FormsAuthentication.SetAuthCookie(viewModel.Login, true);

                return Redirect(FormsAuthentication.DefaultUrl);
            }

            return View(viewModel);
        }
    }
}