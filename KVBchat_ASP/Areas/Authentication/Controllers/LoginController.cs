﻿using BusinessLogic.Service.Base;
using KVBchat_ASP.Areas.Authentication.Models;
using KVBchat_ASP.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KVBchat_ASP.Areas.Authentication.Controllers
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

            if (_authenticationService.Authenticate(viewModel.Login, viewModel.Password))
            {
                FormsAuthentication.SetAuthCookie(viewModel.Login, true);
                return Redirect(FormsAuthentication.DefaultUrl);
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }

        //[HttpGet]
        [ChildActionOnly]
        public PartialViewResult UserLogin()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return PartialView("_UserLogin", new UserViewModel
                {
                    Nickname = Thread.CurrentPrincipal.Identity.Name,
                    IsAuthenticated = true
                });
            }
            return PartialView("_UserLogin");
        }
    }
}