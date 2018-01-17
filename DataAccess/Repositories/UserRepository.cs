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
using Utility;

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

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            SaveChanges();
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUser(Expression<Func<User, bool>> func)
        {
            var user = _context.Users.FirstOrDefault(func);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public IEnumerable<User> GetUsers(IEnumerable<int> users)
        {
            return _context.Users.Where(x => users.Contains(x.Id)).ToArray();
        }

        public IEnumerable<User> GetUsers(Expression<Func<User, bool>> func)
        {
            return _context.Users.Where(func).ToArray();
        }

        public IEnumerable<User> GetUsersFromGroup(int id)
        {
            return _context.UsersGroups.Include(x => x.User).Where(x => x.IdGroup == id).Select(x => x.User).ToArray();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
