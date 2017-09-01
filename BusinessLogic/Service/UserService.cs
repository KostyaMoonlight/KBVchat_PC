using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataAccess.Repositories.Base;

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
    }
}
