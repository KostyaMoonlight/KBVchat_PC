using AutoMapper;
using BusinessLogic.DTO.User;
using BusinessLogic.Service.Base;
using KVBchat_ASP.Models.Login;
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
        public ActionResult Index()
        {
            var user = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name);

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
            _userService.EditUser(userEditView);

            return Redirect("Index");
        }

    }
}