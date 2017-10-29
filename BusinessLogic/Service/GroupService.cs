using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataAccess.Repositories.Base;
using BusinessLogic.DTO.User;
using BusinessLogic.DTO.Group;
using AutoMapper;

namespace BusinessLogic.Service
{
    public class GroupService
        : IGroupService
    {
        IGroupRepository _repository = null;
        IMapper _mapper = null;

        public GroupService(IGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddGroup(int creatorId, IEnumerable<int> members, string name)
        {
            var groupId = _repository.AddGroup(creatorId, name);
            _repository.AddUserGroup(groupId, creatorId);
            foreach (var member in members)
            {
                _repository.AddUserGroup(groupId, member);
            }
            
        }

        public void LeaveGroup(int member, int groupId)
        {
            _repository.RemoveUserFromGroup(member, groupId);
            if (IsGroupEmpty(groupId))
                _repository.RemoveGroupAndMessages(groupId);
        }

        public void AddUserToGroup(int userId, int groupId)
        {
            _repository.AddUserGroup(groupId, userId);
        }

        public Group GetGroup(int id)
        {
            return _repository.GetGroup(id);
        }

        public IEnumerable<Group> GetGroups(string name)
        {
            return _repository.GetGroups(x => x.Name.Contains(name));
        }

        public IEnumerable<GroupViewModel> GetUsersGroups(int id)
        {
            var groups = _repository
                .GetUsersGroups(id)
                .Select(x => _mapper.Map<GroupViewModel>(x))
                .ToList();
            return groups;
        }

        public bool IsGroupEmpty(int groupId)
        {
            var groups = _repository.GetUsersGroupsIncludeUsers(x => x.IdGroup == groupId);
            return !groups.Any();
        }


    }
}
