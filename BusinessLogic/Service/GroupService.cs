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
    }
}
