using BusinessLogic.DTO.Message;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
    public interface IMessageService
    {
        MessageViewModel SendMessage(MessageViewModel message);
        IEnumerable<Message> GetMessages();
        IEnumerable<Message> GetUnreadMessages();        
        IEnumerable<Message> GetMessages(int idSender, int idResiver);
        IEnumerable<Message> GetMessages(int idSender, int idResiver, string text);
        IEnumerable<MessageViewModel> GetMessagesFromGroup(int groupId);
    }
}
