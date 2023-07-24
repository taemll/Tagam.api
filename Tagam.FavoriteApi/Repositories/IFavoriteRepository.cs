using Tagam.FavoriteApi.Models.Dto;

namespace Tagam.FavoriteApi.Repositories
{
    public interface IFavoriteRepository
    {
        Task<FavoriteDto> Create(FavoriteDto favoriteDto);
        Task<string> Remove(int id);
        Task<IEnumerable<FavoriteDto>> GetFavoritesByClientId(Guid id); 
        Task<IEnumerable<FavoriteDto>> GetFavoritesByRecipeId(Guid id); 
    } 
}
