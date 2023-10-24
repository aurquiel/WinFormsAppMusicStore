using AutoMapper;
using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities;

namespace WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence.Entities.Mappings
{
    public class AudioStroreLocalPersistenceMappingProfile : Profile
    {

        public AudioStroreLocalPersistenceMappingProfile()
        {
            CreateMap<AudioListEntity, AudioFile>();
            CreateMap<AudioFile, AudioListEntity>();
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>();
            CreateMap<Register, RegisterEntity>();
            CreateMap<RegisterEntity, Register>();
        }
    }
}
