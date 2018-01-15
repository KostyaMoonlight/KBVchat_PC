using AutoMapper;
using BusinessLogic.DTO.User;
using Domain.Entities;
using KVBchat_ASP.Areas.Authentication.Models;
using KVBchat_ASP.Areas.Cabinet.Models;
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
            CreateMap<User, UserEditViewModel>()
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate));
            CreateMap<UserInfoViewModel, UserEditViewModel>();
            CreateMap<UserInfoViewModel, UserCabinetViewModel>();
            CreateMap<UserInfoViewModel, UserWithdrawViewModel>().
                ForMember(dest => dest.Withdraw, opt => opt.MapFrom(src => 0));
            CreateMap<UserInfoViewModel, UserDepositViewModel>().
                ForMember(dest => dest.Deposit, opt => opt.MapFrom(src => 0));
            CreateMap<UserRegistrationViewModel, User>()
                .ForMember(dest => dest.LastTimeAccess, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UserEditViewModel, User>();
            CreateMap<UserCabinetViewModel, User>();
            CreateMap<UserDepositViewModel, User>();
            CreateMap<UserWithdrawViewModel, User>();
            CreateMap<UserDepositViewModel, UserInfoViewModel>();
            CreateMap<UserWithdrawViewModel, UserInfoViewModel>();
        }

    }
}