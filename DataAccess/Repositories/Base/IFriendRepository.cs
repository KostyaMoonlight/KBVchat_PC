using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Base
{
    public interface IFriendRepository
        :ISaveChanges
    {
        void AddFriends(int firstId, int secondId);
        bool ConfirmeFriend(int firstId, int secondId);
        bool RemoveFriend(int firstId, int secondId);
        IEnumerable<User> GetUsersFriends(int id);
        IEnumerable<int> GetUsersFriendsId(Expression<Func<Friend, bool>> whereIdFunc, Expression<Func<Friend, bool>> whereFunc, Expression<Func<Friend, int>> selectFunc);
    }
}
