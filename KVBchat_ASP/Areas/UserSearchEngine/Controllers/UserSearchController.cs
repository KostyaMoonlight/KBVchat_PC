using BusinessLogic.Service.Base;
using KVBchat_ASP.Areas.UserSearchEngine.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Areas.UserSearchEngine.Controllers
{
    [Authorize]
    public class UserSearchController : Controller
    {
        IUserService _userService = null;

        public UserSearchController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult UserSearch()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult UserSearch(UserSearchViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(viewModel);
            }
            var users = _userService.SearchUsers(viewModel.FullName, viewModel.Age ?? 0);

            return PartialView("_UserSearchResult", users);
        }
    }
}