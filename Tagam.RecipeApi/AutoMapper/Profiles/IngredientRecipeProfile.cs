using AutoMapper;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.AutoMapper.Profiles
{
    public class IngredientRecipeProfile : Profile
    {
        public IngredientRecipeProfile()
        {
            CreateMap<IngredientRecipe, IngredientRecipeDto>()
               .ReverseMap();
            
            CreateMap<IngredientRecipe, IngredientRecipeResponse>()
               .ReverseMap();
        }
    }
}
