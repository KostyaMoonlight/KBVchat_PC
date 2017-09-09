using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Base
{
    public interface IUserRepository: ISaveChanges
    {
        User GetUser(int id);
        User GetUser(Expression<Func<User, bool>> func);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(IEnumerable<int> users);
        IEnumerable<User> GetUsers(Expression<Func<User, bool>> func);
        IEnumerable<User> GetUsersFriends(int id);
        IEnumerable<Group> GetUsersGroups(int id);
        IEnumerable<Group> GetUsersGroups(Expression<Func<Group, bool>> func);
        IEnumerable<Message> GetUsersMessages(int id);
        IEnumerable<User> GetUsersFromGroup(int id);
        void AddUser(User user);

    }
}
