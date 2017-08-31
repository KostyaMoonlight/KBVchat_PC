using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Base
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(IEnumerable<int> users);
        IEnumerable<User> GetUsers(Expression<Func<User, bool>> func);
        User GetUserFullInfo(int id);
        IEnumerable<User> GetUsersFullInfo();
        IEnumerable<User> GetUsersFullInfo(Expression<Func<User, bool>> func);
        IEnumerable<User> GetUsersFriends(int id);
        IEnumerable<Group> GetUsersGroups(int id);
        IEnumerable<Message> GetUsersMessages(int id);
    }
}
