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
                (CurrentUser.Balance > room.DefaultBet * 3))
            {
                room = _pokerService.AddUserToRoom(CurrentUser.Id, CurrentUser.Balance, CurrentUser.Nickname, id);
                var roomWithUser = new PokerWithCurrentPlayerViewModel()
                {
                    PokerViewModel = room,
                    CurrentUserId = CurrentUser.Id
                };
                return View("Room", roomWithUser);
            }
            else
                return View("RoomsList", _pokerService.GetPokerRooms());
        }

        public ActionResult StartNewGame(int id)
        {
            var room = _pokerService.GetRoomState(id);

            _pokerService.RemoveUserFromRoom(CurrentUser.Id, id);

            room = _pokerService.AddUserToRoom(CurrentUser.Id, CurrentUser.Balance,
                CurrentUser.Nickname, id);

            var roomWithUser = new PokerWithCurrentPlayerViewModel()
            {
                PokerViewModel = room,
                CurrentUserId = CurrentUser.Id
            };
            return View("Room", roomWithUser);
        }

        public PartialViewResult Reload(int id)
        {
            var room = _pokerService.GetRoomState(id);
            var roomWithUser = new PokerWithCurrentPlayerViewModel()
            {
                PokerViewModel = room,
                CurrentUserId = CurrentUser.Id
            };
            var player = roomWithUser.PokerViewModel.Players.
                FirstOrDefault(user => user.Id == CurrentUser.Id);
            if (player != null)
                CurrentUser.Balance = player.Balance;
            _userService.EditBalance(_mapper.Map<User>(CurrentUser));

            if (room.IsEnd)
                ViewBag.IsPokerGameFinished = true;
            else
                ViewBag.IsPokerGameFinished = false;
            return PartialView("_Table", roomWithUser);
        }

        public ActionResult RoomsList()
        {
            return View(_pokerService.GetPokerRooms());
        }

        public ActionResult ExitGame(int id)
        {
            _pokerService.RemoveUserFromRoom(CurrentUser.Id, id);
            return View("RoomsList", _pokerService.GetPokerRooms());
        }

        [HttpPost]
        public ActionResult Check(int id)
        {
            _pokerService.Check(id, CurrentUser.Id);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Raise(int id)
        {
            _pokerService.Raise(id, CurrentUser.Id, 20);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Bet(int id)
        {
            _pokerService.Bet(id, CurrentUser.Id, 10);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Fold(int id)
        {
            _pokerService.Fold(id, CurrentUser.Id);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Call(int id)
        {
            _pokerService.Call(id, CurrentUser.Id);
            return new EmptyResult();
        }
    }
}