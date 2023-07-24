using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tagam.RecipeApi.ApiResponses;
using Tagam.RecipeApi.Enums;
using Tagam.RecipeApi.Models.Dto;
using Tagam.RecipeApi.Repositories;

namespace Tagam.RecipeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeStepRepository _stepsRepository;

        public RecipeController(IRecipeRepository recipeRepository, IRecipeStepRepository stepsRepository)
        {
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
            _stepsRepository = stepsRepository ?? throw new ArgumentNullException(nameof(stepsRepository));
        }

        [HttpGet("show-recipes")]
        public async Task<object> GetRecipes()
        {
            var recipe = await _recipeRepository.GetRecipes();
            return new ApiResponse(HttpStatusCode.OK, recipe);
        }

        [HttpGet("show-recipe")]
        public async Task<object> GetRecipeById(Guid id)
        {
            var recipe = await _recipeRepository.GetRecipeById(id);
            return new ApiResponse(HttpStatusCode.OK, recipe);
        }

        [HttpGet("show-recipes-by-query")]
        public async Task<object> GetRecipesByQuery(string query)
        {
            var recipe = await _recipeRepository.GetRecipesByQuery(query);
            return new ApiResponse(HttpStatusCode.OK, recipe);
        }

        [HttpGet("show-recipes-by-category")]
        public async Task<object> GetRecipesByCategory(int id)
        {
            var recipe = await _recipeRepository.GetRecipesByCategory(id);
            return new ApiResponse(HttpStatusCode.OK, recipe);
        }

        [HttpGet("show-recipes-by-time")]
        public async Task<object> GetRecipesByTime(TimeForPreparing time)
        {
            var recipe = await _recipeRepository.GetRecipesByTime(time);
            return new ApiResponse(HttpStatusCode.OK, recipe);
        }
        
        [HttpGet("show-recipes-by-ingredient")]
        public async Task<object> GetRecipesByIngredients(int id)
        {
            var recipe = await _recipeRepository.GetRecipesByIngredients(id);
            return new ApiResponse(HttpStatusCode.OK, recipe);
        }
        
        [HttpGet("show-steps-by-recipeId")]
        public async Task<object> GetStepsByRecipeId(Guid id)
        {
            var steps = await _stepsRepository.GetStepsByRecipeId(id);
            return new ApiResponse(HttpStatusCode.OK, steps);
        }
        
        [HttpGet("show-step-by-id")]
        public async Task<object> GetStepById(int id)
        {
            var steps = await _stepsRepository.GetStepById(id);
            return new ApiResponse(HttpStatusCode.OK, steps);
        }

        [HttpPost("create-recipe")]
        public async Task<object> CreateRecipe(RecipeDto recipe)
        {
            var newrecipe = await _recipeRepository.Create(recipe);
            return new ApiResponse(HttpStatusCode.OK, newrecipe);
        }

        public async Task<object> GetFavoriteRecipes(int id)
        {
            var favorites = await _recipeRepository.GetFavoriteRecipes(id);
            return new ApiResponse(HttpStatusCode.OK,favorites);
        }

        [HttpPost("create-step")]
        public async Task<object> CreateStep(RecipeStepsDto recipe)
        {
            var newStep = await _stepsRepository.Create(recipe);
            return new ApiResponse(HttpStatusCode.OK, newStep);
        }

        [HttpPut("update-recipe/{id}")]
        public async Task<object> UpdateRecipe(RecipeUpdateDto recipe)
        {
            var newrecipe = await _recipeRepository.Update(recipe);
            return new ApiResponse(HttpStatusCode.OK, newrecipe);
        }
        
        [HttpPut("update-step")]
        public async Task<object> UpdateStep(RecipeStepsDto recipe)
        {
            var newStep = await _stepsRepository.Update(recipe);
            return new ApiResponse(HttpStatusCode.OK, newStep);
        }
        
        [HttpDelete("delete-recipe")]
        public async Task<object> DeleteRecipe(Guid id)
        {
            await _recipeRepository.Delete(id);
            return new ApiResponse(HttpStatusCode.OK);
        }
        
        [HttpDelete("delete-step")]
        public async Task<object> DeleteStep(int id)
        {
            await _stepsRepository.Delete(id);
            return new ApiResponse(HttpStatusCode.OK);
        }
    }
}
