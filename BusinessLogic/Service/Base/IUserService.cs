using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
    public interface IUserService
    {
        User GetUser(int id);
        User GetUserFullInfo(int id);
        IEnumerable<User> GetUsersFullInfo(string name);
        IEnumerable<User> GetUsersFriends(int id);
        IEnumerable<Group> GetUsersGroups(int id);
        IEnumerable<Message> GetUsersMessages(int id);
        void UserNotification(IEnumerable<Message> messages);

    }
}
