using BusinessLogic.Service.Base;
using KVBchat_ASP.Models.Group;
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
        IFriendService _friendService = null;

        public GroupController(IGroupService groupService, IUserService userService, IFriendService friendService)
        {
            _groupService = groupService;
            _userService = userService;
            _friendService = friendService;
        }

        [HttpGet]
        public ActionResult Groups()
        {
            var user = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name);
            ViewBag.UserId = user.Id;


            var groups = _groupService.GetUsersGroups(user.Id);

            return PartialView("_Groups", groups);
        }

        [HttpGet]
        public ActionResult AddGroup()
        {
            var currentUserId = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id;
            var friends = _friendService.GetUsersFriends(currentUserId);
            var friendsSelectList = friends.Select(x => new SelectListItem { Text = x.Nickname, Value = x.Id.ToString() });
            var groupCreationViewModel = new GroupCreationViewModel() { CreatorId = currentUserId, Members = friendsSelectList, SelectedMembers = new List<string>() };
            return View(groupCreationViewModel);
        }

        [HttpPost]
        public ActionResult AddGroup(GroupCreationViewModel groupCreationViewModel)
        {
            var currentUserId = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id;
            if (string.IsNullOrWhiteSpace(groupCreationViewModel.Name) || groupCreationViewModel.SelectedMembers == null)                                                                       
            {
                var friends = _friendService.GetUsersFriends(currentUserId);
                var friendsSelectList = friends.Select(x => new SelectListItem { Text = x.Nickname, Value = x.Id.ToString() });
                groupCreationViewModel.Members = friendsSelectList;
                return View(groupCreationViewModel);
            }        
            _groupService.AddGroup(currentUserId, groupCreationViewModel.SelectedMembers.Select(x=>int.Parse(x)), groupCreationViewModel.Name);

            return RedirectToAction("Messages", "Message", new { area = "" });
        }

        [HttpGet]
        public ActionResult LeaveGroup()
        {
            var groupIdObject = TempData.Peek("groupId");
            if (groupIdObject == null)
                return RedirectToAction("Messages", "Message", new { area = "" });

            var groupId = int.Parse(groupIdObject.ToString());
            var currentUserId = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id;
            _groupService.LeaveGroup(currentUserId, groupId);
            return RedirectToAction("Messages", "Message",new { area = "" });
        }

        [HttpPost]
        public ActionResult AddToGroup(int id)
        {

            var groupId = GetGroupId();
            if (groupId != -1)
                _groupService.AddUserToGroup(id, groupId);
            return new EmptyResult();
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