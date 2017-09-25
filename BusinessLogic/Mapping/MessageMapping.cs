using AutoMapper;
using BusinessLogic.DTO.Message;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapping
{
    public class MessageMapping
        :Profile
    {
        public MessageMapping()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.User.Nickname))
                .ForMember(dest => dest.IdSender, opt => opt.MapFrom(src => src.IdSender));

            CreateMap<MessageViewModel, Message>();

            CreateMap<SendMessageViewModel, MessageViewModel>()
                .ForMember(dest => dest.IsDelivered, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
