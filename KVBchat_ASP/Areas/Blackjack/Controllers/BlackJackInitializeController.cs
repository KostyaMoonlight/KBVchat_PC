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
    public class BlackJackInitializeController : Controller
    {
        IBlackjackService _blackjackService = null;
        IUserService _userService = null;

        public BlackJackInitializeController(IBlackjackService blackjackService, IUserService userService)
        {
            _blackjackService = blackjackService;
            _userService = userService;
        }

        public ActionResult InitBJ()
        {
            //if (_userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id != 3)
            //{
            //    return new EmptyResult();
            //}
            for (int i = 0; i < 3; i++)
            {
                _blackjackService.AddRoom(1, 1);
                _blackjackService.AddRoom(1, 2);
                _blackjackService.AddRoom(1, 3);
                _blackjackService.AddRoom(1, 4);
            }
            for (int i = 0; i < 3; i++)
            {
                _blackjackService.AddRoom(10, 1);
                _blackjackService.AddRoom(10, 2);
                _blackjackService.AddRoom(10, 3);
                _blackjackService.AddRoom(10, 4);
            }
            for (int i = 0; i < 3; i++)
            {
                _blackjackService.AddRoom(100, 1);
                _blackjackService.AddRoom(100, 2);
                _blackjackService.AddRoom(100, 3);
                _blackjackService.AddRoom(100, 4);
            }
            for (int i = 0; i < 3; i++)
            {
                _blackjackService.AddRoom(1000, 1);
                _blackjackService.AddRoom(1000, 2);
                _blackjackService.AddRoom(1000, 3);
                _blackjackService.AddRoom(1000, 4);
            }
            return new EmptyResult();
        }
    }
}