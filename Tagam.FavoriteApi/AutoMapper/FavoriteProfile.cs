using AutoMapper;
using Tagam.FavoriteApi.Models;
using Tagam.FavoriteApi.Models.Dto;

namespace Tagam.FavoriteApi.AutoMapper
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<Favorite, FavoriteDto>()
                .ReverseMap();
        }
    }
}
