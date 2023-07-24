using Tagam.RecipeApi.Enums;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<RecipeResponseDto>> GetRecipes();
        Task<RecipeInfoDto> GetRecipeById(Guid id);
        Task<IEnumerable<RecipeResponseDto>> GetRecipesByQuery(string query);
        Task<IEnumerable<RecipeResponseDto>> GetRecipesByCategory(int id);
        Task<IEnumerable<RecipeResponseDto>> GetRecipesByTime(TimeForPreparing time);
        Task<IEnumerable<RecipeResponseDto>> GetRecipesByIngredients(int id);
        Task<RecipeInfoDto> Create(RecipeDto recipe);
        Task<RecipeInfoDto> Update(RecipeUpdateDto recipe);
        Task<string> Delete(Guid id);
        //метод который показывает рецепты по лайкам
        Task<IEnumerable<RecipeResponseDto>> GetFavoriteRecipes(int id);
    }
}
