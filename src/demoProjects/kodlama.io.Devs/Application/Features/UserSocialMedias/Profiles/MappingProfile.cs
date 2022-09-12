using Application.Features.UserSocialMedias.Command.CreateSocialMedia;
using Application.Features.UserSocialMedias.Command.DeleteSocialMedia;
using Application.Features.UserSocialMedias.Command.UpdateSocialMedia;
using Application.Features.UserSocialMedias.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMedias.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserSocialMedia, CreateSocialMediaCommand>().ReverseMap();
            CreateMap<UserSocialMedia, CreatedSocialMediaDto>().ReverseMap();

            CreateMap<UserSocialMedia, DeletedSocialMediaDto>().ReverseMap();
            CreateMap<UserSocialMedia, DeleteSocialMediaCommand>().ReverseMap();

            CreateMap<UserSocialMedia, UpdatedSocialMediaDto>().ReverseMap();
            CreateMap<UserSocialMedia, UpdateSocialMediaCommand>().ReverseMap();
        }
    }
}
