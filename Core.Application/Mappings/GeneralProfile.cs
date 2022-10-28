using AutoMapper;
using Core.Application.ViewModels.PostVM;
using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region user
            CreateMap<User, UserViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());


            CreateMap<User, SaveUserViewModel>()
              .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
              .ReverseMap()
              .ForMember(dest => dest.Created, opt => opt.Ignore())
              .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
              .ForMember(dest => dest.LastModified, opt => opt.Ignore())
              .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
              .ForMember(dest => dest.Friends, opt => opt.Ignore())
              .ForMember(dest => dest.Posts, opt => opt.Ignore());
            #endregion

            #region posts
            CreateMap<Post, SavePostViewModel>()
               .ForMember(dest => dest.File, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
               .ForMember(dest => dest.User, opt => opt.Ignore())
               .ForMember(dest => dest.Comments, opt => opt.Ignore());
            #endregion
        }
    }
}
