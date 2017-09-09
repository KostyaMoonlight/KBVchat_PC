using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataAccess.Repositories.Base;
using BusinessLogic.DTO.User;
using AutoMapper;
using Utility;

namespace BusinessLogic.Service
{
    public class UserService
        : IUserService
    {
        IUserRepository _repository = null;
        IMapper _mapper = null;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void EditUser(UserEditViewModel user)
        {
            var oldUser = _repository.GetUser(user.Id);
            if (oldUser == null)
            {
                return;
            }

            oldUser.Nickname = user.Nickname;
            oldUser.FirstName = user.FirstName;
            oldUser.MiddleName = user.MiddleName;
            oldUser.ThirdName = user.ThirdName;
            oldUser.Birthdate = user.Birthdate;

            _repository.SaveChanges();
        }

        public User GetUser(int id)
        {
            return _repository.GetUser(id);
        }

        public UserInfoViewModel GetUserByLogin(string login)
        {
            return _mapper.Map<UserInfoViewModel>(
                _repository.GetUser(x => x.Email == login || x.Phone == login)
                );
        }

        public IEnumerable<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        public IEnumerable<User> GetUsersFriends(int id)
        {
            return _repository.GetUsersFriends(id);
        }

        public IEnumerable<Group> GetUsersGroups(int id)
        {
            return _repository.GetUsersGroups(id);
        }

        public IEnumerable<Message> GetUsersMessages(int id)
        {
            return _repository.GetUsersMessages(id);
        }

        public bool RegisterUser(UserRegistrationViewModel user)
        {
            var newUser = _mapper.Map<User>(user);
            var oldUser = _repository.GetUser(x => x.Email == user.Email || x.Phone == user.Phone);
            if (oldUser != null)
            {
                return false;
            }
            var newPassword = newUser.Password.EncryptPassword();
            newUser.Password = newPassword;
            _repository.AddUser(newUser);
            return true;
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
