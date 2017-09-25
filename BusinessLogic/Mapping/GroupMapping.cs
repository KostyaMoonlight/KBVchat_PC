using AutoMapper;
using BusinessLogic.DTO.Group;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapping
{
    public class GroupMapping
        :Profile
    {
        public GroupMapping()
        {
            CreateMap<Group, GroupViewModel>()
                .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admin != null ? src.Admin.Nickname : ""));

        }
    }
}
