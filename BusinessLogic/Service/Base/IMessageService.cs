using BusinessLogic.DTO.Message;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
    public interface IMessageService
    {
        void SetAsRead(IEnumerable<int> id);
        MessageViewModel SendMessage(MessageViewModel message, IEnumerable<FileViewModel> files);
        Stream GetFile(string fileId);
        IEnumerable<Message> GetMessages();
        IEnumerable<Message> GetMessages(int idSender, int idResiver);
        IEnumerable<Message> GetMessages(int idSender, int idResiver, string text);
        IEnumerable<Message> GetUnreadMessages();
        IEnumerable<MessageViewModel> GetUnreadMessages(int idGroup, int idUser);
        IEnumerable<Message> GetUsersMessages(int id);
        IEnumerable<MessageViewModel> GetMessagesFromGroup(int groupId);
    }
}
