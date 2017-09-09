using AutoMapper;
using BusinessLogic.DTO.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapping
{
    public class UserMapping
        :Profile
    {

        public UserMapping()
        {
            CreateMap<User, UserInfoViewModel>(); 
            CreateMap<User, UserEditViewModel>(); 
            CreateMap<UserInfoViewModel, User>();
            CreateMap<UserInfoViewModel, UserEditViewModel>();
            CreateMap<UserRegistrationViewModel, User>()
                .ForMember(dest => dest.LastTimeAccess, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
