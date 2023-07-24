using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tagam.FavoriteApi.Database;
using Tagam.FavoriteApi.Models.Dto;

namespace Tagam.FavoriteApi.Repositories.Implementation
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FavoriteRepository> _logger;

        public FavoriteRepository(DataContext context, IMapper mapper, ILogger<FavoriteRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<FavoriteDto> Create(FavoriteDto favoriteDto)
        {
            var newFavorite = _mapper.Map<FavoriteDto>(favoriteDto);
            await _context.AddAsync(newFavorite);
            await _context.SaveChangesAsync();
            return _mapper.Map<FavoriteDto>(newFavorite);
        }

        public async Task<IEnumerable<FavoriteDto>> GetFavoritesByClientId(Guid id)
        {
            var favorites = await _context.Favorites.Where(w=>w.ClientId == id).FirstOrDefaultAsync();
            return _mapper.Map<List<FavoriteDto>>(favorites);
        }

        public async Task<IEnumerable<FavoriteDto>> GetFavoritesByRecipeId(Guid id)
        {
            var favorites = await _context.Favorites.Where(w => w.ClientId == id).FirstOrDefaultAsync();
            return _mapper.Map<List<FavoriteDto>>(favorites);
        }

        public async Task<string> Remove(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite is null)
            {
                throw new ArgumentNullException(nameof(favorite));
            }
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return "success";
        }
    }
}
