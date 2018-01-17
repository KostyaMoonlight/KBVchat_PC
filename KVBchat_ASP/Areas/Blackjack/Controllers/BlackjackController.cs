using AutoMapper;
using BusinessLogic.DTO.User;
using BusinessLogic.Service.Base;
using Domain.Entities;
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

        private UserInfoViewModel CurrentUser { get; set; }

        public BlackjackController(IUserService userService, IBlackjackService blackjackService, IMapper mapper)
        {            
            _userService = userService;
            _blackjackService = blackjackService;
            _mapper = mapper;
            CurrentUser = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name);
        }

        public ActionResult RoomsList()
        {
            return View(_blackjackService.GetBlackJackRooms());
        }

        public ActionResult JoinRoom(int id)
        {
            var room = _blackjackService.GetRoomState(id);
            if ((room.Players.Count < room.MaxPlayersCount) &&
                (CurrentUser.Balance > room.Bet * 2))
            {
                room = _blackjackService.AddUserToRoom(CurrentUser.Id, CurrentUser.Balance, CurrentUser.Nickname, id);
                var roomWithUser = new BlackjackWithCurrentPlayerViewModel()
                {
                    BlackjackViewModel = room,
                    CurrentUserId = CurrentUser.Id
                };
                return View("Room", roomWithUser);
            }
            else
                return View("RoomsList", _blackjackService.GetBlackJackRooms());
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

        public ActionResult ExitGame(int id)
        {
            var room = _blackjackService.GetRoomState(id);
            var roomWithUser = new BlackjackWithCurrentPlayerViewModel()
            {
                BlackjackViewModel = room,
                CurrentUserId = CurrentUser.Id
            };
            var player = roomWithUser.BlackjackViewModel.Players.
                FirstOrDefault(user => user.Id == CurrentUser.Id);
            CurrentUser.Balance = player.Balance;

            _userService.EditBalance(_mapper.Map<User>(CurrentUser));
            _blackjackService.RemoveUserFromRoom(CurrentUser.Id, id);
            return View("RoomsList", _blackjackService.GetBlackJackRooms());
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
