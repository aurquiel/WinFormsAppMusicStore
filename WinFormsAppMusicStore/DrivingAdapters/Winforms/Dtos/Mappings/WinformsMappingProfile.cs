using AutoMapper;
using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters.Entities;

namespace WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms.Dtos.Mappings
{
    public class WinfromsMappingProfile : Profile
    {
        public WinfromsMappingProfile()
        {
            CreateMap<AudioFile, AudioFileSelect>();
            CreateMap<AudioFileSelect, AudioFile>();
            CreateMap<AudioFileSelect, AudioFileSelectPlayed>();
            CreateMap<AudioFileSelectPlayed, AudioFileSelect>();
        }
    }
}
