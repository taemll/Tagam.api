using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<IngredientDto>> GetIngredients();
        Task<IngredientDto> CreateIngredient(IngredientDto ingredient);
    }
}
