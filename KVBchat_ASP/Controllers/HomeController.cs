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
    public class HomeController : Controller
    {
        IUserService _userService = null;
        IGroupService _groupService = null;
        IMessageService _messageService = null;
        IMapper _mapper;

        public HomeController(
            IUserService userService,
            IGroupService groupService,
            IMessageService messageService,
            IMapper mapper
            )
        {
            _userService = userService;
            _groupService = groupService;
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index(int id = -1)
        {
            UserInfoViewModel user;
            
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

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModel userEditView)
        {
            _userService.EditUser(_mapper.Map<User>(userEditView));

            return Redirect("Index");
        }

    }
}