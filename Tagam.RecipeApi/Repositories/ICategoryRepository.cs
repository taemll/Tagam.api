using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategoryById(int id);
        Task<CategoryDto> CreateCategory(CategoryDto category);
    }
}
