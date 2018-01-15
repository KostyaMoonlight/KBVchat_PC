using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Areas.Poker.Controllers
{
    public class PokerController : Controller
    {
        // GET: Poker/Poker
        public ActionResult JoinRoom()
        {
            return View("Room");
        }
    }
}