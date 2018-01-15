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
using BusinessLogic.DTO.Group;

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

        public void EditUser(User user)
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
            oldUser.Balance = user.Balance;
            oldUser.Card = user.Card;
            oldUser.CardDate = user.CardDate;
            oldUser.CardSVV = user.CardSVV;

            _repository.SaveChanges();
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

        public bool RegisterUser(User user)
        {
            var oldUser = _repository.GetUser(x => x.Email == user.Email || x.Phone == user.Phone);
            if (oldUser != null)
            {
                return false;
            }
            var newPassword = user.Password.EncryptPassword();
            user.Password = newPassword;
            _repository.AddUser(user);
            return true;
        }

        public IEnumerable<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        public IEnumerable<UserShortInfoViewModel> SearchUsers(string fullName, int age)
        {
            return _repository.GetUsers
                (
                    x => (age == 0 || (DateTime.Now.Year - x.Birthdate.Year) == age) &&
                    (
                        x.Nickname.Contains(fullName) ||
                        x.FirstName.Contains(fullName) ||
                        x.MiddleName.Contains(fullName) ||
                        x.ThirdName.Contains(fullName)
                    )
                ).Select(x => _mapper.Map<UserShortInfoViewModel>(x));
        }

        public IEnumerable<FriendShortInfoViewModel> GetUsersFromGroup(int groupId)
        {
            return _repository.GetUsersFromGroup(groupId).Select(x=> _mapper.Map<FriendShortInfoViewModel>(x));
        }
    }
}
