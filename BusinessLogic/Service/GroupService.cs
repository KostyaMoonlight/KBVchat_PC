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
    public class GroupService
        : IGroupService
    {
        IGroupRepository _repository = null;

        public GroupService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public Group GetGroup(int id)
        {
            return _repository.GetGroup(id);
        }

        public IEnumerable<Group> GetGroups(string name)
        {
            return _repository.GetGroups(x => x.Name.Contains(name));
        }

    }
}
