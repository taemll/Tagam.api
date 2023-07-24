using Tagam.RatingApi.Models.Dto;

namespace Tagam.RatingApi.Repositories
{
    public interface IRatingRepository
    {
        Task<string> AddRatingToRecipe(RatingDto rating);
        Task<double> CalculateAverageRating(Guid recipeId);
    }
}
