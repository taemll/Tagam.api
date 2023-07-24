using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories
{
    public interface IRecipeStepRepository
    {
        Task<IEnumerable<RecipeStepsDto>> GetStepsByRecipeId(Guid id);
        Task<RecipeStepsDto> GetStepById(int id);
        Task<RecipeStepsDto> Create(RecipeStepsDto step);
        Task<RecipeStepsDto> Update(RecipeStepsDto step);
        Task<string> Delete(int id);
    }
} 
