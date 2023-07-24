using AutoMapper;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.AutoMapper.Profiles
{
    public class TypeKitchenProfile : Profile
    {
        public TypeKitchenProfile()
        {
            CreateMap<TypeKitchen, TypeKitchenDto>()
                .ReverseMap();
        }
    }
}