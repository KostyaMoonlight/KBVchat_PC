using BusinessLogic.DTO.Group;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
    public interface IGroupService
    {
        void AddGroup(int creatorId, IEnumerable<int> members, string name);
        void LeaveGroup(int member, int groupId);
        void AddUserToGroup(int userId, int groupId);
        Group GetGroup(int id);
        IEnumerable<Group> GetGroups(string name);
        IEnumerable<GroupViewModel> GetUsersGroups(int id);
        bool IsGroupEmpty(int groupId);
    }
}
