using AutoMapper;
using Tagam.ImageApi.Models;
using Tagam.ImageApi.Models.Dto;

namespace Tagam.ImageApi.AutoMapper.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageDto>()
            .ReverseMap();
        }
    }
}
