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
    public class FriendsController : Controller
    {
        IUserService _userService = null;

        public FriendsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult GetFriends()
        {
            var user = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name);

            var friends = _userService.GetUsersFriendsShortInfo(user.Id);

            return View(friends);
        }
    }
}