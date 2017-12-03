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

        public Message SendMessage(Message message)
        {
            var mess = _context.Messages.Add(message);
            SaveChanges();
            return mess;
        }

        public void AddFile(string name, string fileId)
        {
            _context.Files.Add(new File { FileName = name, FileId = fileId });
            SaveChanges();
        }

        public int GetFileIdByName(string fileId)
        {
            return _context.Files.FirstOrDefault(x => x.FileId == fileId).Id;
        }

        public void AddMessageFile(int messageId, int fileId)
        {
            _context.MessageFiles.Add(new MessageFile { IdMessage = messageId, IdFile = fileId });
            SaveChanges();
        }

        public IEnumerable<File> GetFilesFromMessage(int messageId)
        {
            return _context.MessageFiles
                .Include(x => x.File)
                .Where(x => x.IdMessage == messageId)
                .Select(x => x.File)
                .ToList();
        }

        public IEnumerable<Message> GetMessages()
        {
            return _context.Messages.ToArray();
        }

        public IEnumerable<Message> GetMessages(Expression<Func<Message, bool>> func)
        {
            return _context.Messages.Where(func).ToArray();
        }

        public IEnumerable<Message> GetMessages(int idSender, int idResiver)
        {
            return _context.Messages.Where(x => x.IdSender == idSender || x.IdGroup == idResiver).ToArray();
        }

        public IEnumerable<Message> GetMessages(int idSender, int idResiver, Expression<Func<Message, bool>> func)
        {
            return _context.Messages.Where(x => x.IdSender == idSender || x.IdGroup == idResiver).Where(func).ToArray();
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

        public IEnumerable<Message> GetUnreadMessages(int idGroup, int idSender)
        {
            return _context.Messages
                .Include(x => x.User)
                .Where(x => x.IdGroup == idGroup)
                .Where(x => x.IdSender != idSender)
                .Where(x => x.IsRead == false)
                .ToList();
        }

        public IEnumerable<Message> GetMessagesIncludeUsers(Expression<Func<Message, bool>> func)
        {
            return _context.Messages.Include(x => x.User).Where(func).ToArray();
        }

        public IQueryable<Message> GetMessages(IEnumerable<int> idCollection)
        {
            return _context.Messages.Where(x => idCollection.Contains(x.Id));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


    }
}
