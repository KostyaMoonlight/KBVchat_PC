using AutoMapper;
using BusinessLogic.Service.Base;
using Domain.Entities;
using KVBchat_ASP.Areas.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KVBchat_ASP.Areas.Authentication.Controllers
{
    public class RegisterController
        : Controller
    {
        IUserService _userService = null;
        IMapper _mapper = null;

        public RegisterController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
            if (_userService.RegisterUser(_mapper.Map<User>(user)))
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            return View(user);
        }
    }
}