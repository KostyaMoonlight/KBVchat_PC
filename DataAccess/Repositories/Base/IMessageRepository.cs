using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Base
{
    public interface IMessageRepository: ISaveChanges
    {
        Message SendMessage(Message message);
        void AddFile(string name, string fileId);
        int GetFileIdByName(string fileId);
        void AddMessageFile(int messageId, int fileId);
        IEnumerable<File> GetFilesFromMessage(int messageId);
        IEnumerable<Message> GetMessages();
        IEnumerable<Message> GetMessages(Expression<Func<Message, bool>> func);
        IEnumerable<Message> GetMessages(int idSender, int idResiver);
        IEnumerable<Message> GetMessages(int idSender, int idResiver, Expression<Func<Message, bool>> func);
        IEnumerable<Message> GetUsersMessages(int id);
        IEnumerable<Message> GetUnreadMessages(int idGroup, int idSender);
        IEnumerable<Message> GetMessagesIncludeUsers(Expression<Func<Message, bool>> func);
        IQueryable<Message> GetMessages(IEnumerable<int> idCollection);

    }
}
