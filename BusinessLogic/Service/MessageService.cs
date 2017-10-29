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

namespace BusinessLogic.Service
{
    public class MessageService
        : IMessageService
    {
        IMessageRepository _repository = null;
        IMapper _mapper = null;

        public MessageService(IMessageRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
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

        public MessageViewModel SendMessage(MessageViewModel message)
        {
            var group = message.IdGroup;
            var mess = _mapper.Map<Message>(message);

            _repository.SendMessage(mess);

            return _mapper.Map<MessageViewModel>(mess);
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
            return _repository
                .GetMessagesIncludeUsers(x => x.IdGroup == groupId)
                .Select(x => _mapper.Map<MessageViewModel>(x));
        }

    }
}
