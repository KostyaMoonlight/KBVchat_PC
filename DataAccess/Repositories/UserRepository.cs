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
    public class UserRepository
        : IUserRepository
    {
        KVBchatDbContext _context = null;

        public UserRepository(KVBchatDbContext context)
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserFullInfo(int id)
        {
            return _context.Users.Include(x=>x.UserInfo).FirstOrDefault(x => x.Id == id);

        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToArray();
        }

        public IEnumerable<User> GetUsers(IEnumerable<int> users)
        {
            return _context.Users.Where(x=>users.Contains(x.Id)).ToArray();
        }

        public IEnumerable<User> GetUsers(Expression<Func<User, bool>> func)
        {
            return _context.Users.Where(func).ToArray();
        }

        public IEnumerable<User> GetUsersFriends(int id)
        {
            var users = _context.Friends   
                .Where(x => x.IdFirst == id)
                .Select(x=>x.IdSecond)
                .ToList();

            users.AddRange(
                _context.Friends
                .Where(x => x.IdSecond == id)
                .Select(x => x.IdFirst)
                .ToList()
                );

            return GetUsers(users);

        }

        public IEnumerable<User> GetUsersFullInfo()
        {
            return _context.Users.Include(x=>x.UserInfo).ToArray();
        }

        public IEnumerable<User> GetUsersFullInfo(Expression<Func<User, bool>> func)
        {
            return _context.Users.Include(x=>x.UserInfo).Where(func).ToArray();
        }

        public IEnumerable<Group> GetUsersGroups(int id)
        {
            var groups = _context.UsersGroups
                .Where(x => x.IdUser == id)
                .Select(x => x.IdGroup)
                .ToList();
            return _context.Groups.Where(x => groups.Contains(x.Id)).ToArray();
        }

        public IEnumerable<Message> GetUsersMessages(int id)
        {
            var groups = GetUsersGroups(id).Select(x=>x.Id).ToList();
            var messages = _context.Messages.Where(x => x.IdSender == id).ToList();
            messages.AddRange(
                _context.Messages.Where(x=> groups.Contains(x.IdGroup)).ToList()
                );
            return messages;
        }
    }
}
