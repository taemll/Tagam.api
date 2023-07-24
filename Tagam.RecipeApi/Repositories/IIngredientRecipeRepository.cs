using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories
{
    public interface IIngredientRecipeRepository
    {
        Task<IngredientRecipeResponse> CreateIngredient(IngredientRecipeDto ingredient);
        Task<IEnumerable<IngredientRecipeResponse>> GetIngredientsByRecipeId(Guid id);
        Task<bool> Delete(int id);
        Task<IEnumerable<IngredientRecipeResponse>> GetIngredientsByIngredientId(int id);
    }
}
