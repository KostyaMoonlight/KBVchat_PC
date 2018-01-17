using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BusinessLogic.Service;
using BusinessLogic.Service.Base;
using BusinessLogic.DTO.User;
using System.Threading;
using KVBchat_ASP.Areas.Poker.Models;
using Domain.Entities;

namespace KVBchat_ASP.Areas.Poker.Controllers
{
    [Authorize]
    public class PokerController : Controller
    {
        IUserService _userService = null;
        IPokerService _pokerService = null;
        IMapper _mapper = null;

        private UserInfoViewModel CurrentUser { get; set; }

        public PokerController(IUserService userService, IPokerService pokerService, IMapper mapper)
        {
            _userService = userService;
            _pokerService = pokerService;
            _mapper = mapper;
            CurrentUser = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name);
        }

        public ActionResult JoinRoom(int id)
        {
            var room = _pokerService.GetRoomState(id);
            if ((room.Players.Count < room.MaxPlayersCount) &&
                (CurrentUser.Balance > room.Bet * 2))
            {
                room = _pokerService.AddUserToRoom(CurrentUser.Id, CurrentUser.Balance, CurrentUser.Nickname, id);
                var roomWithUser = new PokerWithCurrentPlayerViewModel()
                {
                    BlackjackViewModel = room,
                    CurrentUserId = CurrentUser.Id
                };
                return View("Room", roomWithUser);
            }
            else
                return View("RoomsList", _pokerService.GetPokerRooms());
        }

        public ActionResult RoomsList()
        {
            var rooms = _pokerService.GetPokerRooms();
            return View(rooms);
        }

        public ActionResult ExitGame(int id)
        {
            var room = _pokerService.GetRoomState(id);
            var roomWithUser = new PokerWithCurrentPlayerViewModel()
            {
                BlackjackViewModel = room,
                CurrentUserId = CurrentUser.Id
            };
            var player = roomWithUser.BlackjackViewModel.Players.
                FirstOrDefault(user => user.Id == CurrentUser.Id);
            CurrentUser.Balance = player.Balance;

            _userService.EditBalance(_mapper.Map<User>(CurrentUser));
            _pokerService.RemoveUserFromRoom(CurrentUser.Id, id);
            return View("RoomsList", _pokerService.GetPokerRooms());
        }
    }
}