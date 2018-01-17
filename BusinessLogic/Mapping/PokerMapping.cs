using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker;
using BusinessLogic.DTO.Poker;

namespace BusinessLogic.Mapping
{
    public class PokerMapping
           : Profile
    {
        public PokerMapping()
        {
            CreateMap<Game, PokerViewModel>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Winners, opt => opt.Ignore());
            CreateMap<Game, PokerRoomSearchViewModel>();
        }
    }
}
