using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataAccess.Repositories.Base;
using BusinessLogic.DTO.User;

namespace BusinessLogic.Service
{
    public class UserService
        : IUserService
    {
        IUserRepository _repository = null;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User GetUser(int id)
        {
            return _repository.GetUser(id);
        }

        public User GetUserFullInfo(int id)
        {
            return _repository.GetUserFullInfo(id);
        }

        public IEnumerable<User> GetUsersFriends(int id)
        {
            return _repository.GetUsersFriends(id);
        }

        public IEnumerable<User> GetUsersFullInfo(string name)
        {
            return _repository.GetUsersFullInfo(x => x.UserInfo.FirstName.Contains(name));
        }

        public IEnumerable<Group> GetUsersGroups(int id)
        {
            return _repository.GetUsersGroups(id);
        }

        public IEnumerable<Message> GetUsersMessages(int id)
        {
            return _repository.GetUsersMessages(id);
        }

        public void UserNotification(IEnumerable<Message> messages)
        {
            var groupsIds = messages.Select(x => x.IdGroup);
            var userUnreadMessages = new List<UserUnreadMessage>();
            foreach (var groupId in groupsIds)
            {
                var users = _repository.GetUsersFromGroup(groupId);
                foreach (var user in users)
                {
                    user.UnreadMessages++;
                }
            }
            _repository.SaveChanges();
        }
    }
}
