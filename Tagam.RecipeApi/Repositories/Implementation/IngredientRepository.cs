using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tagam.RecipeApi.Database;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories.Implementation
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<IngredientRepository> _logger;
        public IngredientRepository(DataContext context, IMapper mapper, ILogger<IngredientRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IngredientDto> CreateIngredient(IngredientDto ingredient)
        {
            _logger.LogInformation(nameof(CreateIngredient));

            var domainIngredient = _mapper.Map<Ingredient>(ingredient);
            var createdIngredient = await _context.Ingredients.AddAsync(domainIngredient);
            await _context.SaveChangesAsync();

            return _mapper.Map<IngredientDto>(createdIngredient.Entity);
        }

        public async Task<IEnumerable<IngredientDto>> GetIngredients()
        {
            _logger.LogInformation(nameof(GetIngredients));
            var ingredients = await _context.Ingredients.OrderBy(r => r.Id).ToListAsync();

            return _mapper.Map<List<IngredientDto>>(ingredients);
        }
    }
}
