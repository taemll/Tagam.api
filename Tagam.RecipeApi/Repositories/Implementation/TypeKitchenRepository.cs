using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tagam.RecipeApi.Database;
using Tagam.RecipeApi.Models;
using Tagam.RecipeApi.Models.Dto;

namespace Tagam.RecipeApi.Repositories.Implementation
{
    public class TypeKitchenRepository : ITypeKitchenRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TypeKitchenRepository> _logger;

        public TypeKitchenRepository(DataContext context, ILogger<TypeKitchenRepository> logger, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TypeKitchenDto>> GetTypesKitchen()
        {
            var typeKitchen = await _context.TypesKitchen.OrderBy(r => r.Id).ToListAsync();
            return _mapper.Map<IEnumerable<TypeKitchenDto>>(typeKitchen);
        }

        public async Task<TypeKitchenDto> GetTypeKitchenById(int id)
        {
            var typeKitchen = await _context.TypesKitchen.FindAsync(id);

            if (typeKitchen is null)
            {
                _logger.LogInformation("typeKitchen is null");

                throw new ArgumentNullException($"Тип кухни с данным ID = {id} не найден");
            }

            return _mapper.Map<TypeKitchenDto>(typeKitchen);
        }

        public async Task<TypeKitchenDto> CreateTypeKitchen(TypeKitchenDto typeKitchen)
        {
            _logger.LogInformation(nameof(CreateTypeKitchen));

            var createdType = _mapper.Map<TypeKitchen>(typeKitchen);
            try
            {
                await _context.TypesKitchen.AddAsync(createdType);
                await _context.SaveChangesAsync();
                return _mapper.Map<TypeKitchenDto>(createdType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, createdType);
                return _mapper.Map<TypeKitchenDto>(ex.Message);
            }
        }
    }
}
