    using Microsoft.EntityFrameworkCore;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Database;
using Tagam.RecipeApi.Models.Dto;
using AutoMapper;

namespace Tagam.RecipeApi.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(DataContext context, IMapper mapper, ILogger<CategoryRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            _logger.LogInformation(nameof(GetCategories));
            var categories = await _context.Categories.OrderBy(r => r.Id).ToListAsync();

            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            _logger.LogInformation(nameof(GetCategoryById));
            var category = await _context.Categories.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (category is null)
            {
                _logger.LogInformation("category is null");

                throw new ArgumentNullException($"По данному ID = {id} отстуствуют категории");
            }

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategory(CategoryDto category)
        {
            _logger.LogInformation(nameof(CreateCategory));

            var createdCategory = _mapper.Map<Category>(category);
            try
            {
                await _context.Categories.AddAsync(createdCategory);
                await _context.SaveChangesAsync();
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, createdCategory);
                return _mapper.Map<CategoryDto>(ex.Message);
            }
        }
    }
}
