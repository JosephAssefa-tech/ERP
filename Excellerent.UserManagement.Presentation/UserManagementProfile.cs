using AutoMapper;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Enums;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using Excellerent.UserManagement.Presentation.Models.PostModel;
using Excellerent.UserManagement.Presentation.Models.PutModels;
using System;

namespace Excellerent.UserManagement.Presentation
{
    public class UserManagementProfile: Profile
    {
        //User profile
        public UserManagementProfile()
        {
            CreateMap<UserPostModel, UserEntity>();
            CreateMap<UserPutModel, UserEntity>()
                .ForMember(e => e.Status, opt => opt.MapFrom(s => Enum.Parse(typeof(UserStatus), s.Status)));
            CreateMap<UserEntity, UserGetModel>()
                .ForMember(e => e.Status, opt => opt.MapFrom(s => Enum.GetName(typeof(UserStatus), s.Status)));
        }
    }
}
