using AutoMapper;
using Blackjack;
using BusinessLogic.DTO.BJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapping
{
    public class BJMapping
        :Profile
    {
        public BJMapping()
        {
            CreateMap<Game, BlackjackViewModel>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src=>0))
                .ForMember(dest => dest.Winners, opt => opt.Ignore());
        }
    }
}
