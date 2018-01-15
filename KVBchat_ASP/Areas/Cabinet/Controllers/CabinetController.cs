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
    [Authorize]
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

        public ActionResult Deposit()
        {
            var user = _mapper.Map<UserDepositViewModel>(
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

        [HttpPost]
        public ActionResult Withdraw(UserWithdrawViewModel userWithdrawViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(userWithdrawViewModel);
            }
            if (userWithdrawViewModel.Withdraw <= 0 ||
                userWithdrawViewModel.Withdraw > userWithdrawViewModel.Balance)
            {
                ViewBag.Orror = "Withdraw must be greater than zero and less then account balance";
                return View(userWithdrawViewModel);
            }
            userWithdrawViewModel.Balance -= userWithdrawViewModel.Withdraw;

            _userService.EditBalance(_mapper.Map<User>(userWithdrawViewModel));

            return Redirect("Cabinet");
        }

        [HttpPost]
        public ActionResult Deposit(UserDepositViewModel userDepositViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(userDepositViewModel);
            }
            if (userDepositViewModel.Deposit <= 0)
            {
                ViewBag.Orror = "Deposit must be greater then zero";
                return View(userDepositViewModel);
            }
            userDepositViewModel.Balance += userDepositViewModel.Deposit;

            _userService.EditBalance(_mapper.Map<User>(userDepositViewModel));
            return Redirect("Cabinet");
        }

        [HttpPost]
        public ActionResult EditCard(UserCabinetViewModel userCabinetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userCabinetViewModel);
            }

            var dates = userCabinetViewModel.CardExpirationDate.Split('/');

            string s = "";
            foreach (var item in DateTime.Now.Year.ToString().Skip(2))
                s += item;
            int currentYear = int.Parse(s);

            if (currentYear > int.Parse(dates[1]) ||
               (currentYear == int.Parse(dates[1]) && DateTime.Now.Month > int.Parse(dates[0])))
            {
                ViewBag.Orror = "Incorect card expiration date";
                return View(userCabinetViewModel);
            }

            _userService.EditUserCard(_mapper.Map<User>(userCabinetViewModel));

            return Redirect("Cabinet");
        }


    }
}