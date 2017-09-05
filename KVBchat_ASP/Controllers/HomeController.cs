using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public HomeController(IUserService userService, IGroupService groupService, IMessageService messageService)
        {
            _userService = userService;
            _groupService = groupService;
            _messageService = messageService;
        }

        public ActionResult Index()
        {
            var users = _userService.GetUsers();
            var groups = _groupService.GetGroup(1);
            var messages = _messageService.GetMessages();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}