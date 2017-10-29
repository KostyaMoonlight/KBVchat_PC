using BusinessLogic.Service.Base;
using KVBchat_ASP.Areas.UserSearchEngine.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Areas.UserSearchEngine.Controllers
{
    [Authorize]
    public class UserSearchController : Controller
    {
        IUserService _userService = null;
        IFriendService _friendService = null;

        public UserSearchController(IUserService userService, IFriendService friendService)
        {
            _userService = userService;
            _friendService = friendService;
        }

        [HttpGet]
        public ActionResult UserSearch()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult UserSearch(UserSearchViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(viewModel);
            }
            var users = _userService.SearchUsers(viewModel.FullName, viewModel.Age ?? 0);
            var userId = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id;
            ViewBag.friendsIds = _friendService.GetUsersFriendsIds(userId);

            return PartialView("_UserSearchResult", users);
        }

        [HttpPost]
        public ActionResult AddFriend(int id)
        {
            var userId = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id;

            _friendService.AddFriend(userId, id);

            return new EmptyResult();
        }
    }
}