using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tagam.RatingApi.ApiResponses;
using Tagam.RatingApi.Models.Dto;
using Tagam.RatingApi.Repositories;

namespace Tagam.RatingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : Controller
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository ?? throw new ArgumentNullException(nameof(ratingRepository));
        }

        [HttpPost("addRating")]
        public async Task<object> AddRatingToRecipe(RatingDto rating)
        {
            try
            {
                var newRating = await _ratingRepository.AddRatingToRecipe(rating);
                return new ApiResponse(System.Net.HttpStatusCode.OK, newRating);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet("calculate")]
        public async Task<object> CalculateAverageRating(Guid recipeId)
        {
            try
            {
                var result = await _ratingRepository.CalculateAverageRating(recipeId);
                return new ApiResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
