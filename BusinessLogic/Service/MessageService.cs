using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataAccess.Repositories.Base;
using BusinessLogic.DTO.Message;
using AutoMapper;
using System.IO;
using GoogleDriveAPI.Service.Base;

namespace BusinessLogic.Service
{
    public class MessageService
        : IMessageService
    {
        IMessageRepository _repository = null;
        IGoogleDriveApiService _googleDriveApiService = null;
        IMapper _mapper = null;

        public MessageService(IMessageRepository repository, IGoogleDriveApiService googleDriveApiService, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _googleDriveApiService = googleDriveApiService;
        }

        public void SetAsRead(IEnumerable<int> id)
        {
            var messages = _repository.GetMessages(id);
            foreach (var message in messages)
            {
                message.IsRead = true;
            }
            _repository.SaveChanges();
        }

        public MessageViewModel SendMessage(MessageViewModel message, IEnumerable<FileViewModel> files)
        {
            var group = message.IdGroup;
            var mess = _mapper.Map<Message>(message);

            var messWithId = _repository.SendMessage(mess);

            var messViewModel = _mapper.Map<MessageViewModel>(mess);


            if (files != null)
            {
                foreach (var file in files)
                {
                    var gId = _googleDriveApiService.UploadFile(file.Name, file.Content);
                    _repository.AddFile(file.Name, gId);
                    int fileId = _repository.GetFileIdByName(gId);
                    _repository.AddMessageFile(messWithId.Id, fileId);
                    messViewModel.Files.Add(new FileDownloadViewModel { FileId = gId, FileName = file.Name });
                }

            }
            
            
            return messViewModel;
        }

        public Stream GetFile(string fileId)
        {
            return _googleDriveApiService.DownloadFile(fileId);
        }

        public IEnumerable<Message> GetMessages()
        {
            return _repository.GetMessages();
        }

        public IEnumerable<Message> GetMessages(int idSender, int idResiver)
        {
            return _repository.GetMessages(idSender, idResiver);
        }

        public IEnumerable<Message> GetMessages(int idSender, int idResiver, string text)
        {
            return _repository.GetMessages(idSender, idResiver, x => x.Text.Contains(text));
        }

        public IEnumerable<Message> GetUnreadMessages()
        {
            return _repository.GetMessages(x => x.IsDelivered == false);
        }

        public IEnumerable<MessageViewModel> GetUnreadMessages(int idGroup, int idUser)
        {
            return _repository.GetUnreadMessages(idGroup, idUser).Select(x => _mapper.Map<MessageViewModel>(x));
        }

        public IEnumerable<Message> GetUsersMessages(int id)
        {
            return _repository.GetUsersMessages(id);
        }

        public IEnumerable<MessageViewModel> GetMessagesFromGroup(int groupId)
        {
            var messages = _repository
                .GetMessagesIncludeUsers(x => x.IdGroup == groupId)
                .Select(x => _mapper.Map<MessageViewModel>(x)).ToList();

            foreach (var message in messages)
            {
                var files = _repository.GetFilesFromMessage(message.Id).ToList();
                if (files != null && files.Count() > 0)
                {
                    for (var i = 0; i < files.Count(); i++)
                    {
                        messages.FirstOrDefault(x => x.Id == message.Id)
                            .Files
                            .Add(new FileDownloadViewModel
                            {
                                FileId = files[i].FileId,
                                FileName = files[i].FileName
                            });
                    }
                }
            }

            return messages;
        }

    }
}
