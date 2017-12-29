using AutoMapper;
using BusinessLogic.DTO.User;
using BusinessLogic.Service.Base;
using KVBchat_ASP.Areas.Blackjack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Areas.Blackjack.Controllers
{
    [Authorize]
    public class BlackjackController : Controller
    {
        IUserService _userService = null;
        IBlackjackService _blackjackService = null;
        IMapper _mapper = null;

        private UserInfoViewModel CurrentUser { get => _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name); }

        public BlackjackController(IUserService userService, IBlackjackService blackjackService, IMapper mapper)
        {
            _userService = userService;
            _blackjackService = blackjackService;
            _mapper = mapper;

            _blackjackService.AddRoom(100);
        }

        public ActionResult JoinRoom(int id)
        {
            _blackjackService.AddRoom(200);
            var room = _blackjackService.AddUserToRoom(CurrentUser.Id, CurrentUser.Nickname, id);
            var roomWithUser = new BlackjackWithCurrentPlayerViewModel()
            {
                BlackjackViewModel = room,
                CurrentUserId = CurrentUser.Id
            };
            return View("Room", roomWithUser);
        }

        public PartialViewResult Reload(int id)
        {
            var room = _blackjackService.GetRoomState(id);
            var roomWithUser = new BlackjackWithCurrentPlayerViewModel()
            {
                BlackjackViewModel = room,
                CurrentUserId = CurrentUser.Id
            };
            return PartialView("_Table", roomWithUser);
        }

        [HttpPost]
        public ActionResult Double(int id)
        {
            _blackjackService.Double(id, CurrentUser.Id);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Hit(int id)
        {
            _blackjackService.Hit(id, CurrentUser.Id);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Stand(int id)
        {
            _blackjackService.Stand(id, CurrentUser.Id);
            return new EmptyResult();
        }

    }
}
