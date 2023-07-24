using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tagam.RecipeApi.Database;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories.Implementation
{
    public class RecipeStepRepository : IRecipeStepRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeStepRepository> _logger;

        public RecipeStepRepository(DataContext context, IMapper mapper, ILogger<RecipeStepRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RecipeStepsDto> GetStepById(int id)
        {
            _logger.LogInformation(nameof(GetStepById));
            var step = await _context.RecipeSteps.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (step is null)
            {
                _logger.LogInformation("step is null");

                throw new ArgumentNullException($"Шаг не найден");
            }

            return _mapper.Map<RecipeStepsDto>(step);
        }

        public async Task<IEnumerable<RecipeStepsDto>> GetStepsByRecipeId(Guid id)
        {
            _logger.LogInformation(nameof(GetStepsByRecipeId));
            var recipes = await _context.RecipeSteps.Where(w => w.RecipeId == id).ToArrayAsync();

            if (recipes is null)
            {
                _logger.LogInformation("recipes is null");

                throw new ArgumentNullException($"По данному ID = {id} отстуствуют шаги");
            }

            return _mapper.Map<List<RecipeStepsDto>>(recipes);
        }

        public async Task<RecipeStepsDto> Create(RecipeStepsDto step)
        {
            _logger.LogInformation(nameof(Create));

            var recipeStep = _mapper.Map<RecipeSteps>(step);
            try
            {
                await _context.RecipeSteps.AddAsync(recipeStep);
                await _context.SaveChangesAsync();
                return _mapper.Map<RecipeStepsDto>(recipeStep);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, recipeStep);
                return _mapper.Map<RecipeStepsDto>(ex.Message);
            }
        }

        public async Task<RecipeStepsDto> Update(RecipeStepsDto step)
        {
            var recipeStep = _mapper.Map<RecipeSteps>(step);
            try
            {
                _context.RecipeSteps.Update(recipeStep);
                await _context.SaveChangesAsync();
                return _mapper.Map<RecipeStepsDto>(recipeStep);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, recipeStep);
                return _mapper.Map<RecipeStepsDto>(ex.Message);
            }
        }

        public async Task<string> Delete(int id)
        {
            _logger.LogInformation(nameof(Delete));

            var recipeStep = await _context.RecipeSteps.FindAsync(id);

            if (recipeStep is null)
            {
                _logger.LogInformation("recipeStep is null");
                throw new ArgumentNullException(nameof(recipeStep));
            }

            try
            {
                _context.RecipeSteps.Remove(recipeStep);
                await _context.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "error";
            }
        }
    }
}
