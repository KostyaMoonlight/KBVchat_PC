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
                .ForMember(dest => dest.AdminName, 
                    opt => opt.MapFrom(src => src.Admin != null ? src.Admin.Nickname : ""))
                .ForMember(dest => dest.UnreadMessagesCount,
                    opt => opt.MapFrom(src => src.Messages.Where(um => um.IsRead == false).Count()))
                .ForMember(dest => dest.LastSenderId, opt => opt.MapFrom(src => src.Messages.Last().IdSender));

        }
    }
}
