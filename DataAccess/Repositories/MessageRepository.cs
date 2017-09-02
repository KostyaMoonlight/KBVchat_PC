using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Linq.Expressions;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class MessageRepository
        : IMessageRepository
    {
        KVBchatDbContext _context = null;

        public MessageRepository(KVBchatDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Message> GetMessages(int idSender, int idResiver)
        {
            return _context.Messages.Where(x => x.IdSender == idSender || x.IdGroup == idResiver).ToArray();
        }

        public IEnumerable<Message> GetMessages(int idSender, int idResiver, Expression<Func<Message, bool>> func)
        {
            return _context.Messages.Where(x => x.IdSender == idSender || x.IdGroup == idResiver).Where(func).ToArray();
        }

        public IEnumerable<Message> GetMessages()
        {
            return _context.Messages.ToArray();
        }

        public IEnumerable<Message> GetMessages(Expression<Func<Message, bool>> func)
        {
            return _context.Messages.Where(func).ToArray();

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
