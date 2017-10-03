using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Linq.Expressions;
using DataAccess.Context;
using System.Data.Entity;

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

        public IEnumerable<Message> GetMessagesIncludeUsers(Expression<Func<Message, bool>> func)
        {
            return _context.Messages.Include(x => x.User).Where(func).ToArray();
        }

        public IEnumerable<Message> GetUsersMessages(int id)
        {
            var groups = _context.UsersGroups
                .Include(x => x.Group)
                .Where(x => x.IdUser == id)
                .Select(x => x.Group.Id)
                .ToList();
            var messages = _context.Messages.Where(x => x.IdSender == id).ToList();
            messages.AddRange(
                _context.Messages.Where(x => groups.Contains(x.IdGroup)).ToList()
                );
            return messages;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void SendMessage(Message message)
        {
            _context.Messages.Add(message);
            SaveChanges();
        }
    }
}
