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

        public ActionResult JoinRoom()
        {
            return View("Room");
        }

        public ActionResult RoomsList()
        {
            var rooms = _pokerService.GetPokerRooms();
            return View(rooms);
        }
    }
}