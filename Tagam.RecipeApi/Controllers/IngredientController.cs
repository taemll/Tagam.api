using Microsoft.AspNetCore.Mvc;
using Tagam.RecipeApi.Models.Dto;
using Tagam.RecipeApi.Repositories;
using Tagam.RecipeApi.ApiResponses;
using System.Net;

namespace Tagam.RecipeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IIngredientRecipeRepository _ingredientRecipeRepository;

        public IngredientController(IIngredientRepository ingredientRepository, IIngredientRecipeRepository ingredientRecipeRepository)
        {
            _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
            _ingredientRecipeRepository = ingredientRecipeRepository ?? throw new ArgumentNullException(nameof(ingredientRecipeRepository));
        }

        [HttpPost("create")]
        public async Task<object> CreateIngredient(IngredientDto ingredient)
        {
            try
            {
                var newIngredient = await _ingredientRepository.CreateIngredient(ingredient);
                return new ApiResponse(HttpStatusCode.OK, newIngredient);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet("show")]
        public async Task<object> ShowIngredients()
        {
            var ingredients = await _ingredientRepository.GetIngredients();
            return new ApiResponse(HttpStatusCode.OK, ingredients);
        }

        [HttpPut("recipe/create")]
        public async Task<object> CreateIngredientRecipe(IngredientRecipeDto ingredient)
        {
            try
            {
                var newIngredient = await _ingredientRecipeRepository.CreateIngredient(ingredient);
                return new ApiResponse(HttpStatusCode.OK, newIngredient);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet("recipe/showByRecipeId")]
        public async Task<object> GetIngredientsByRecipeId(Guid id)
        {
            try
            {
                var ingredient = await _ingredientRecipeRepository.GetIngredientsByRecipeId(id);
                return new ApiResponse(HttpStatusCode.OK, ingredient);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        
        [HttpGet("recipe/showByIngredientId")]
        public async Task<object> GetIngredientsByIngredientId(int id)
        {
            try
            {
                var ingredient = await _ingredientRecipeRepository.GetIngredientsByIngredientId(id);
                return new ApiResponse(HttpStatusCode.OK, ingredient);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete("recipe/delete")]
        public async Task<object> Delete(int id)
        {
            try
            {
                await _ingredientRecipeRepository.Delete(id);
                return new ApiResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
