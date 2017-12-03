using AutoMapper;
using BusinessLogic.DTO.Message;
using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Utility;

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

        [HttpPost]
        public PartialViewResult SendMessage(SendMessageViewModel message)
        {

            var fileStreams = TempData["Files"];

            if (string.IsNullOrWhiteSpace(message.Text) && fileStreams == null)
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
            var serverMessage = _messageService.SendMessage(newMessage, (fileStreams as List<FileViewModel>));
            IEnumerable<MessageViewModel> viewModel = new List<MessageViewModel>
            {
                serverMessage
            };

            return PartialView("_GroupMessages", viewModel);
        }

        [HttpPost]
        public void Upload()
        {
            IEnumerable<FileViewModel> streams = new List<FileViewModel>();
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    Stream stream = new MemoryStream();
                    upload.InputStream.CopyTo(stream);
                    (streams as List<FileViewModel>).Add(new FileViewModel { Name = upload.FileName.SelectFileName(), Content = stream });
                }
            }
            if (streams.Count() > 0)
            {
                if (TempData.Keys.Contains("Files"))
                {
                    TempData.Remove("Files");
                }
                TempData.Add("Files", streams);
            }

        }

       
        public FileResult DownloadFile(string fileId, string fileName)
        {
            Stream stream = null;
            byte[] bytes = null;
            try
            {
                stream = _messageService.GetFile(fileId);
                stream.Position = 0;
                using (BinaryReader br = new BinaryReader(stream))
                {
                    bytes = br.ReadBytes((int)stream.Length);
                }
                return File(bytes, "application/unknown", fileName);
            }
            finally
            {
                
                stream.Close();
            }
        }

        public PartialViewResult GroupMessages(int id)
        {
            var messages = _messageService.GetMessagesFromGroup(id);
            var lastMessageSenderId = messages.LastOrDefault() != null ? messages.LastOrDefault().IdSender : -1;
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


    }
}