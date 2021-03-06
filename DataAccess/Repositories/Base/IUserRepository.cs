﻿using Domain.Entities;
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
        void AddUser(User user);
        User GetUser(int id);
        User GetUser(Expression<Func<User, bool>> func);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(IEnumerable<int> users);
        IEnumerable<User> GetUsers(Expression<Func<User, bool>> func);
        IEnumerable<User> GetUsersFromGroup(int id);

    }
}
