using AutoMapper;
using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities.Mappings
{
    public class WebserviceMappingProfile : Profile
    {
        public WebserviceMappingProfile() 
        {
            CreateMap<AudioFile, AudioFileDto>();
            CreateMap<AudioFileDto, AudioFile>();
            CreateMap<UserAccess, UserAccessDto>();
            CreateMap<UserAccessDto, UserAccess>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Register, RegisterDto>();
            CreateMap<RegisterDto, Register>();
        }
    }
}
