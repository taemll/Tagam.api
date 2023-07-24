using AutoMapper;
using Tagam.RatingApi.Models;
using Tagam.RatingApi.Models.Dto;

namespace Tagam.RatingApi.AutoMapper.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingDto>()
                .ReverseMap();
        }
    }
}