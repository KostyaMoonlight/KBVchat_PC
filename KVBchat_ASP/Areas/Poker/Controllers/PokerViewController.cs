using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Areas.Poker.Controllers
{
    public class PokerViewController : Controller
    {
        // GET: Poker/PokerView
        public ActionResult Index()
        {
            return View();
        }
    }
}