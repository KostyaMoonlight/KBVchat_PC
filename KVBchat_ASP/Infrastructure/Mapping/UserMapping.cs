using AutoMapper;
using BusinessLogic.DTO.User;
using Domain.Entities;
using KVBchat_ASP.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KVBchat_ASP.Infrastructure.Mapping
{
    public class UserMapping
        : Profile

    {
        public UserMapping()
        {
            CreateMap<User, UserEditViewModel>();
            CreateMap<UserInfoViewModel, UserEditViewModel>();
            CreateMap<UserRegistrationViewModel, User>()
                .ForMember(dest => dest.LastTimeAccess, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UserEditViewModel, User>();
        }

    }
}