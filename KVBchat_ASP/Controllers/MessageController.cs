using AutoMapper;
using BusinessLogic.DTO.Message;
using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        IMessageService _messageService = null;
        IUserService _userService = null;
        IMapper _mapper = null;

        public MessageController(IMessageService messageService, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Messages()
        {
            return View();
        }

        public PartialViewResult GroupMessages(int id)
        {
            var messages = _messageService.GetMessagesFromGroup(id);
            var lastMessageSenderId = messages.Last().IdSender;
            var userId = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id;
            if (userId != lastMessageSenderId)
            {
                var unreadMessagesId = messages.Where(x => x.IsRead == false);
                _messageService.SetAsRead(unreadMessagesId.Select(x => x.Id));
                foreach (var mess in messages)
                {
                    mess.IsRead = true;
                }
            }
            TempData.Clear();
            TempData.Add("groupId", id);

            return PartialView("_GroupMessages", messages);
        }

        public PartialViewResult GroupUnreadMessages()
        {
            var userId = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id;
            var groupId = TempData.Peek("groupId");
            if (groupId == null)
            {
                return PartialView("_GroupMessages", null);
            }
            var id = int.Parse(groupId.ToString());
            var messages = _messageService.GetUnreadMessages(id, userId);

            var unreadMessagesId = messages.Where(x => x.IsRead == false).Select(x => x.Id);
            _messageService.SetAsRead(unreadMessagesId);
            foreach (var mess in messages)
            {
                mess.IsRead = true;
            }

            TempData.Clear();
            TempData.Add("groupId", id);

            return PartialView("_GroupMessages", messages);
        }

        [HttpPost]
        public PartialViewResult SendMessage(SendMessageViewModel message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
            {
                return null;
            }
            var newMessage = _mapper.Map<MessageViewModel>(message);
            newMessage.IsRead = false;

            var groupId = TempData.Peek("groupId");
            if (groupId == null)
            {
                return null;
            }

            newMessage.IdGroup = Convert.ToInt32(groupId);
            newMessage.IdSender = _userService.GetUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id;
            var serverMessage = _messageService.SendMessage(newMessage);
            IEnumerable<MessageViewModel> viewModel = new List<MessageViewModel>
            {
                serverMessage
            };

            return PartialView("_GroupMessages", viewModel);
        }
    }
}