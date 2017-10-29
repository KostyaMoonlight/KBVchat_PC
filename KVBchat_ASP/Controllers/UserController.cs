using AutoMapper;
using BusinessLogic.DTO.User;
using BusinessLogic.Service.Base;
using Domain.Entities;
using KVBchat_ASP.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        IUserService _userService = null;
        IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public new ActionResult User(int id = -1)
        {
            UserInfoViewModel user = null;

            if (id == -1)
            {
                user = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name);
                ViewBag.User = true;
            }
            else
            {
                user = _mapper.Map<UserInfoViewModel>(_userService.GetUser(id));
                ViewBag.User = false;
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var user = _mapper.Map<UserEditViewModel>(
                _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name)
                );
            TempData.Clear();
            TempData.Add("Birthdate", user.Birthdate.Value.Date);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModel userEditView)
        {
            if (!ModelState.IsValid)
            {
                return View(userEditView);
            }
            if (userEditView.Birthdate == null)
            {
                userEditView.Birthdate = DateTime.Parse(TempData["Birthdate"].ToString());
            }
            else
            {
                TempData.Remove("Birthdate");
            }
            _userService.EditUser(_mapper.Map<User>(userEditView));

            return Redirect("User");
        }

    }
}