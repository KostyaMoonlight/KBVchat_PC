using BusinessLogic.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
    public interface IFriendService
    {
        void AddFriend(int firstId, int secondId);
        void ConfirmeFriend(int firstId, int secondId);
        void RemoveFriend(int firstId, int secondId);
        IEnumerable<int> GetUsersFriendsIds(int id);
        IEnumerable<int> GetUncofirmedFriendsIds(int id);
        IEnumerable<FriendViewModel> GetUsersFriends(int id);
        IEnumerable<FriendShortInfoViewModel> GetUsersFriendsShortInfo(int id);


    }
}
