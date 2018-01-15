using AutoMapper;
using BusinessLogic.DTO.User;
using BusinessLogic.Service.Base;
using Domain.Entities;
using KVBchat_ASP.Areas.Cabinet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Areas.Cabinet.Controllers
{
    public class CabinetController : Controller
    {
        IUserService _userService = null;
        IMapper _mapper;

        public CabinetController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public ActionResult Cabinet(int id = -1)
        {
            UserCabinetViewModel user = null;

            if (id == -1)
            {
                user = _mapper.Map<UserCabinetViewModel>(
                    _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name));

                ViewBag.User = true; 
            }
            else
            {
                user = _mapper.Map<UserCabinetViewModel>(_userService.GetUser(id));
                ViewBag.User = false;
            }

            return View(user);
        }

        public ActionResult Withdraw()
        {
            var user = _mapper.Map<UserWithdrawViewModel>(
                _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name)
                );
            return View(user);
        }

        [HttpPost]
        public ActionResult Withdraw(UserWithdrawViewModel userWithdrawViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(userWithdrawViewModel);
            }
            if (userWithdrawViewModel.Withdraw < 0 || userWithdrawViewModel.Withdraw > userWithdrawViewModel.Balance)
            {
                return View(userWithdrawViewModel);
            }
            userWithdrawViewModel.Balance -= userWithdrawViewModel.Withdraw;
            _userService.EditUser(_mapper.Map<User>(userWithdrawViewModel));

            return Redirect("Cabinet");
        }

        public ActionResult Deposit()
        {
            var user = _mapper.Map<UserCabinetViewModel>(
               _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name)
               );
            return View(user);
        }

        public ActionResult EditCard()
        {
            var user = _mapper.Map<UserCabinetViewModel>(
               _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name)
               );
            return View(user);
        }
    }
}