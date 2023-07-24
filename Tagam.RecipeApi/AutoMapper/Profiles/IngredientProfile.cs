using AutoMapper;
using Tagam.RecipeApi.Models.Dto;
using Tagam.RecipeApi.Models;

namespace Tagam.RecipeApi.AutoMapper.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>()
                .ReverseMap();
        }
    }
}
