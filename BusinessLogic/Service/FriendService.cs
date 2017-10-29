using AutoMapper;
using BusinessLogic.DTO.User;
using BusinessLogic.Service.Base;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class FriendService
        :IFriendService
    {
        IFriendRepository _repository = null;
        IMapper _mapper = null;

        public FriendService(IFriendRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddFriend(int firstId, int secondId)
        {
            var friend = _repository.GetUsersFriends(firstId).FirstOrDefault(x => x.Id == secondId);

            if (friend == null)
            {
                _repository.AddFriends(firstId, secondId);
            }
        }

        public void ConfirmeFriend(int firstId, int secondId)
        {
            if (!_repository.ConfirmeFriend(firstId, secondId))
                _repository.ConfirmeFriend(secondId, firstId);
        }

        public void RemoveFriend(int firstId, int secondId)
        {
            if (!_repository.RemoveFriend(firstId, secondId))
                _repository.RemoveFriend(secondId, firstId);
        }

        public IEnumerable<int> GetUsersFriendsIds(int id)
        {
            return _repository.GetUsersFriends(id).Select(x => x.Id);
        }

        public IEnumerable<int> GetUncofirmedFriendsIds(int id)
        {
            return _repository.GetUsersFriendsId(
                x => x.IdSecond == id,
                x => x.IsConfirmed == false,
                x => x.IdFirst
                );
        }

        public IEnumerable<FriendViewModel> GetUsersFriends(int id)
        {
            return _repository.GetUsersFriends(id).Select(x => _mapper.Map<FriendViewModel>(x));
        }

        public IEnumerable<FriendShortInfoViewModel> GetUsersFriendsShortInfo(int id)
        {
            return _repository
                .GetUsersFriends(id)
                .Select(x => _mapper
                .Map<FriendShortInfoViewModel>(x));
        }

    }
}
