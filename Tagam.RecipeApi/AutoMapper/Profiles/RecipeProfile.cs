using AutoMapper;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.AutoMapper.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>()
                .ReverseMap();

            CreateMap<Recipe, RecipeResponseDto>()
                .ReverseMap();
            
            CreateMap<RecipeDto, RecipeResponseDto>()
                .ReverseMap();
            
            CreateMap<Recipe, RecipeUpdateDto>()
                .ReverseMap();
                        
            CreateMap<RecipeDto, RecipeUpdateDto>()
                .ReverseMap();
            
            CreateMap<RecipeResponseDto, RecipeUpdateDto>()
                .ReverseMap();
            
            CreateMap<Recipe, RecipeInfoDto>()
                .ReverseMap();
        }
    }
}
