using AutoMapper;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.AutoMapper.Profiles
{
    public class RecipeStepsProfile : Profile
    {
        public RecipeStepsProfile()
        {
            CreateMap<RecipeSteps, RecipeStepsDto>()
                .ReverseMap();
        }
    }
}
