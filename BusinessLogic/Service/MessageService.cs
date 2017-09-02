using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataAccess.Repositories.Base;

namespace BusinessLogic.Service
{
    public class MessageService
        : IMessageService
    {
        IMessageRepository _repository = null;

        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Message> GetMessages(int idSender, int idResiver)
        {
            return _repository.GetMessages(idSender, idResiver);
        }

        public IEnumerable<Message> GetMessages(int idSender, int idResiver, string text)
        {
            return _repository.GetMessages(idSender, idResiver, x=>x.Text.Contains(text));
        }

        public IEnumerable<Message> GetMessages()
        {
            return _repository.GetMessages();
        }

        public IEnumerable<Message> GetUnreadMessages()
        {
            return _repository.GetMessages(x => x.IsDelivered == false);
        }
    }
}
