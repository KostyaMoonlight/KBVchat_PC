using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        IGroupService _groupService = null;
        IUserService _userService = null;

        public GroupController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Groups()
        {
            var user = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name);

            var groups = _groupService.GetUsersGroups(user.Id);

            return PartialView("_Groups", groups);
        }
    }
}