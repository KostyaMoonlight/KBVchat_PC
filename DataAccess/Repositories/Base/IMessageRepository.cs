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
        IEnumerable<Message> GetMessages();
        IEnumerable<Message> GetMessages(Expression<Func<Message, bool>> func);
        IEnumerable<Message> GetMessages(int idSender, int idResiver);
        IEnumerable<Message> GetMessages(int idSender, int idResiver, Expression<Func<Message, bool>> func);
    }
}
