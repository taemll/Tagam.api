using AutoMapper;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.AutoMapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
            
            CreateMap<object, CategoryDto>()
                .ReverseMap();
        }
    }
}
