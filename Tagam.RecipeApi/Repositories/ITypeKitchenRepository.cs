using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories
{
    public interface ITypeKitchenRepository
    {
        Task<IEnumerable<TypeKitchenDto>> GetTypesKitchen();
        Task<TypeKitchenDto> GetTypeKitchenById(int id);
        Task<TypeKitchenDto> CreateTypeKitchen(TypeKitchenDto typeKitchen);
    }
}
