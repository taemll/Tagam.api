using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System;
using Tagam.RecipeApi.Database;
using Tagam.RecipeApi.Enums;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;
using System.IO;

namespace Tagam.RecipeApi.Repositories.Implementation
{
    public class RecipeRepository : IRecipeRepository
    {
        static HttpClient client = new HttpClient();
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeRepository> _logger;

        public RecipeRepository(DataContext context, IMapper mapper, ILogger<RecipeRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RecipeInfoDto> GetRecipeById(Guid id)
        {
            _logger.LogInformation(nameof(GetRecipeById));
            var recipe = await _context.Recipes.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (recipe is null)
            {
                _logger.LogInformation("recipe is null");

                throw new ArgumentNullException($"рецепт не найден");
            }
            var dtoRecipe = _mapper.Map<Recipe>(recipe);
            dtoRecipe.Category = await _context.Categories.FindAsync(recipe.CategoryId);
            dtoRecipe.TypeKitchen = await _context.TypesKitchen.FindAsync(recipe.TypeKitchenId);
            dtoRecipe.IngredientRecipes = await _context.IngredientRecipes.Where(w=>w.RecipeId == recipe.Id).ToListAsync();
            dtoRecipe.Steps = await _context.RecipeSteps.Where(w=>w.RecipeId==recipe.Id).ToListAsync();
            return _mapper.Map<RecipeInfoDto>(dtoRecipe);
        }  

        public async Task<IEnumerable<RecipeResponseDto>> GetRecipes()
        {
            _logger.LogInformation(nameof(GetRecipes));
            var recipes = await _context.Recipes.OrderBy(r => r.Id).ToListAsync();

            return _mapper.Map<List<RecipeResponseDto>>(recipes);
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetRecipesByCategory(int id)
        {
            _logger.LogInformation(nameof(GetRecipesByCategory));
            var recipes = await _context.Recipes.Where(w => w.CategoryId == id).ToListAsync();

            if (recipes is null)
            {
                _logger.LogInformation("recipes is null");

                throw new ArgumentNullException($"рецепт не найден");
            }

            return _mapper.Map<List<RecipeResponseDto>>(recipes);
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetRecipesByIngredients(int id)
        {
            _logger.LogInformation(nameof(GetRecipesByIngredients));
            var ingredientRecipe = await _context.IngredientRecipes.Where(w => w.IngredientId == id).ToListAsync();

            var recipes = await _context.Recipes.Where(w => ingredientRecipe.Select(s=>s.RecipeId).Contains(w.Id)).ToListAsync();
           
            return _mapper.Map<IEnumerable<RecipeResponseDto>>(recipes);
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetRecipesByQuery(string query)
        {
            var recipes = await _context.Recipes.Where(w => w.Name == query || w.Description.Contains(query)).ToArrayAsync();
            if (recipes is null)
            {
                _logger.LogInformation("recipes is null");

                throw new ArgumentNullException($"рецепт не найден");
            }

            return _mapper.Map<List<RecipeResponseDto>>(recipes);
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetRecipesByTime(TimeForPreparing time)
        {
            var recipes = await _context.Recipes.Where(w => w.TimeForPreparing == time).ToArrayAsync();
            if (recipes is null)
            {
                _logger.LogInformation("recipes is null");

                throw new ArgumentNullException($"рецепт не найден");
            }

            return _mapper.Map<List<RecipeResponseDto>>(recipes);
        }

        public async Task<RecipeInfoDto> Create(RecipeDto recipe)
        {
            var newRecipe = _mapper.Map<Recipe>(recipe);
            try
            {
                newRecipe.Category = await _context.Categories.Where(w => w.Id == recipe.CategoryId).FirstOrDefaultAsync();
                newRecipe.TypeKitchen = await _context.TypesKitchen.Where(w => w.Id == recipe.TypeKitchenId).FirstOrDefaultAsync();
                await _context.Recipes.AddAsync(newRecipe);
                await _context.SaveChangesAsync();
                return _mapper.Map<RecipeInfoDto>(newRecipe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, newRecipe);
                return _mapper.Map<RecipeInfoDto>(ex.Message);
            }
        }

        public async Task<RecipeInfoDto> Update(RecipeUpdateDto recipe)
        {
            if (recipe is null)
                throw new ArgumentNullException(nameof(recipe));

            var domainProduct = _mapper.Map<Recipe>(recipe);
            var updatedProduct = _context.Recipes.Update(domainProduct);
            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeInfoDto>(updatedProduct.Entity);
        }

        public async Task<string> Delete(Guid id)
        {
            _logger.LogInformation(nameof(Delete));

            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe is null)
            {
                _logger.LogInformation("recipeStep is null");
                throw new ArgumentNullException(nameof(recipe));
            }

            try
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "error";
            }
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetFavoriteRecipes(int id)
        {
            var url = $"https://localhost:7150/api/Favorite/show/byCLient?id={id}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IEnumerable<FavoritResponseDto>>();
                var recipes = await _context.Recipes.Where(w => result.Select(s=>s.ClientId).Contains(w.Id)).ToListAsync();
                return _mapper.Map<List<RecipeResponseDto>>(recipes);
            }
            throw new ArgumentException("Something went wrong");
            
        }

        /*public async Task<IEnumerable<RecipeDto>> GetMyRecipes(Guid id)
        {
            _logger.LogInformation(nameof(GetRecipeById));
            var recipes = await _context.Recipes.Where(w => w.ClientId == id).FirstOrDefaultAsync();

            if (recipes is null)
            {
                _logger.LogInformation("recipes is null");

                throw new ArgumentNullException($"рецепт не найден");
            }

            return _mapper.Map<List<RecipeDto>>(recipes);
        } */


        public async Task<IEnumerable<RecipeResponseDto>> GetTopRecipes()
        {
            var recipes = await _context.Recipes.OrderByDescending(u => u.Id).Take(5).ToListAsync();
            return _mapper.Map<List<RecipeResponseDto>>(recipes);
        }
    }
}
