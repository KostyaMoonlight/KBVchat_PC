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
        IFriendService _friendService = null;
        IUserService _userService = null;
        IGroupService _groupService = null;
        int _currentUserId;

        public FriendsController(IFriendService friendService, IUserService userService, IGroupService groupService)
        {
            _friendService = friendService;
            _userService = userService;
            _groupService = groupService;
            _currentUserId = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id;
        }

        [HttpGet]
        public ActionResult GetFriends()
        {

            var friends = _friendService.GetUsersFriendsShortInfo(_currentUserId);
            var UnconfirmedFriends = _friendService.GetUncofirmedFriendsIds(_currentUserId);
            ViewBag.uncofrirmedFriends = UnconfirmedFriends;
            return View(friends);
        }

        [HttpPost]
        public ActionResult ConfirmeFriend(int id)
        {
            _friendService.ConfirmeFriend(_currentUserId, id);

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteFriend(int id)
        {
            _friendService.RemoveFriend(_currentUserId, id);

            return new EmptyResult();
        }

        [HttpGet]
        //[ChildActionOnly]
        public ActionResult GetFriendsOutOfGroup()
        {
            var groupId = GetGroupId();
            if (groupId == -1)
                return new EmptyResult();

            var friends = _friendService.GetUsersFriendsShortInfo(_currentUserId);
            var friendsInGroup = _userService.GetUsersFromGroup(groupId);
            var friendsOutOfGroup = friends.Where(x => !friendsInGroup.Select(i=> i.Id).Contains(x.Id));
            return PartialView("_GetFriendsOutOfGroup", friendsOutOfGroup);
        }

        private int GetGroupId()
        {
            var groupIdObject = TempData.Peek("groupId");
            if (groupIdObject == null)
                return -1;
            var groupId = int.Parse(groupIdObject.ToString());
            return groupId;
        }
    }
}