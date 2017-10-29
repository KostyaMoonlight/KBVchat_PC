using BusinessLogic.DTO.Group;
using BusinessLogic.DTO.User;
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
        void EditUser(User user);
        void UserNotification(IEnumerable<Message> messages);
        User GetUser(int id);
        UserInfoViewModel GetUserByLogin(string login);
        bool RegisterUser(User user);
        IEnumerable<User> GetUsers();
        IEnumerable<UserShortInfoViewModel> SearchUsers(string fullName, int age);
        IEnumerable<FriendShortInfoViewModel> GetUsersFromGroup(int groupId);

    }
}
