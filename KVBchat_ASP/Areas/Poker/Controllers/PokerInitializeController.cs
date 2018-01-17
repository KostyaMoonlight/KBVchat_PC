using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Service.Base;

namespace KVBchat_ASP.Areas.Poker.Controllers
{
    [Authorize]
    public class PokerInitializeController : Controller
    {

        IPokerService _pokerService = null;
        IUserService _userService = null;

        public PokerInitializeController(IPokerService pokerService, IUserService userService)
        {
            _pokerService = pokerService;
            _userService = userService;
        }

        public ActionResult InitPoker()
        {
            //if (_userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id != 3)
            //{
            //    return new EmptyResult();
            //}
            for (int i = 0; i < 3; i++)
            {
                _pokerService.AddRoom(1, 2);
                _pokerService.AddRoom(1, 2);
                _pokerService.AddRoom(1, 3);
                _pokerService.AddRoom(1, 4);
            }
            for (int i = 0; i < 3; i++)
            {
                _pokerService.AddRoom(10, 1);
                _pokerService.AddRoom(10, 2);
                _pokerService.AddRoom(10, 3);
                _pokerService.AddRoom(10, 4);
            }
            for (int i = 0; i < 3; i++)
            {
                _pokerService.AddRoom(100, 1);
                _pokerService.AddRoom(100, 2);
                _pokerService.AddRoom(100, 3);
                _pokerService.AddRoom(100, 4);
            }
            for (int i = 0; i < 3; i++)
            {
                _pokerService.AddRoom(1000, 1);
                _pokerService.AddRoom(1000, 2);
                _pokerService.AddRoom(1000, 3);
                _pokerService.AddRoom(1000, 4);
            }
            return new EmptyResult();
        }
    }
}
