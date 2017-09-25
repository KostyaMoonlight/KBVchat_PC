using AutoMapper;
using BusinessLogic.DTO.Group;
using BusinessLogic.DTO.Message;
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
        : Profile
    {

        public UserMapping()
        {
            CreateMap<User, UserInfoViewModel>();
            CreateMap<UserInfoViewModel, User>();
            CreateMap<User, FriendViewModel>();
            CreateMap<User, FriendShortInfoViewModel>();
            }
    }
}
