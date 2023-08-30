using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using Tagam.RecipeApi.Database;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories.Implementation
{
    public class IngredientRecipeRepository : IIngredientRecipeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<IngredientRecipeRepository> _logger;

        public IngredientRecipeRepository(DataContext context, IMapper mapper, ILogger<IngredientRecipeRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IngredientRecipeResponse> CreateIngredient(IngredientRecipeDto ingredient)
        {
            _logger.LogInformation(nameof(CreateIngredient));

            var domainIngredient = _mapper.Map<IngredientRecipe>(ingredient);
            domainIngredient.Ingredient = await _context.Ingredients.FindAsync(ingredient.IngredientId);
            var createdIngredienty = await _context.IngredientRecipes.AddAsync(domainIngredient);
            await _context.SaveChangesAsync();

            return _mapper.Map<IngredientRecipeResponse>(createdIngredienty.Entity);
        }

        public async Task<bool> Delete(int id)
        {
            _logger.LogInformation("method delete started");

            if (id == 0)
                throw new ArgumentException(nameof(id));

            var ingredientRecipe = await _context.IngredientRecipes.FindAsync(id);

            if (ingredientRecipe is null)
            {
                _logger.LogInformation("ingredient is null");

                throw new ArgumentNullException(nameof(ingredientRecipe));
            }

            try
            {
                _context.IngredientRecipes.Remove(ingredientRecipe);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return false;
            }
        }

        public async Task<IEnumerable<IngredientRecipeResponse>> GetIngredientsByRecipeId(Guid id)
        {
            _logger.LogInformation(nameof(GetIngredientsByRecipeId));
            var ingredientRecipe = await _context.IngredientRecipes.Where(w => w.RecipeId == id).ToListAsync();
            if (ingredientRecipe is null)
            {
                _logger.LogInformation("ingredientRecipe is null");

                throw new ArgumentNullException($"По данному ID = {id} отстуствуют ингредиенты");
            }
            foreach (var ingredirent in ingredientRecipe)
            {
                ingredirent.Ingredient = _context.Ingredients.Where(x => ingredientRecipe.Select(s => s.IngredientId).Contains(x.Id)).FirstOrDefault();
            }
            return _mapper.Map<IEnumerable<IngredientRecipeResponse>>(ingredientRecipe);
        }

        public async Task<IEnumerable<IngredientRecipeResponse>> GetIngredientsByIngredientId(int id)
        {
            _logger.LogInformation(nameof(GetIngredientsByIngredientId));
            var ingredientRecipe = await _context.IngredientRecipes.Where(w => w.IngredientId == id).ToListAsync();
            if (ingredientRecipe is null)
            {
                _logger.LogInformation("ingredientRecipe is null");

                throw new ArgumentNullException($"По данному ID = {id} отстуствуют ингредиенты");
            }
            foreach (var ingredirent in ingredientRecipe)
            {
                ingredirent.Ingredient = _context.Ingredients.Where(x => ingredientRecipe.Select(s => s.IngredientId).Contains(x.Id)).FirstOrDefault();
            }
            return _mapper.Map<IEnumerable<IngredientRecipeResponse>>(ingredientRecipe);
        }
    }
} 