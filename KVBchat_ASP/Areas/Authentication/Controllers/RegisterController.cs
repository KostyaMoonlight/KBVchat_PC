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

            var dates = user.CardExpirationDate.Split('/');

            string s = "";
            foreach (var item in DateTime.Now.Year.ToString().Skip(2))
                s += item;
            int currentYear = int.Parse(s);

            if (currentYear > int.Parse(dates[1]) ||
               (currentYear == int.Parse(dates[1]) && DateTime.Now.Month > int.Parse(dates[0])))
            {
                ViewBag.Orror = "Incorect card expiration date";
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