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
        User GetUser(int id);
        UserInfoViewModel GetUserByLogin(string login);
        void EditUser(User user);
        bool RegisterUser(User user);
        IEnumerable<User> GetUsers();
        IEnumerable<FriendViewModel> GetUsersFriends(int id);
        IEnumerable<FriendShortInfoViewModel> GetUsersFriendsShortInfo(int id);
        IEnumerable<UserShortInfoViewModel> SearchUsers(string fullName, int age);
        void UserNotification(IEnumerable<Message> messages);

    }
}
