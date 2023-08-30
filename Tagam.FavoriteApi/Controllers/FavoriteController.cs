using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tagam.FavoriteApi.ApiResponses;
using Tagam.FavoriteApi.Models.Dto;
using Tagam.FavoriteApi.Repositories;

namespace Tagam.FavoriteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository ?? throw new ArgumentNullException(nameof(favoriteRepository)); ;
        }

        [HttpPost("create")]
        public async Task<object> Create(FavoriteDto favoriteDto)
        {
            var favorite = await _favoriteRepository.Create(favoriteDto);
            return new ApiResponse(HttpStatusCode.OK, favorite);
        }

        [HttpPost("delete")]
        public async Task<object> Remove(int id)
        {
            var favorite = await _favoriteRepository.Remove(id);
            return new ApiResponse(HttpStatusCode.OK, favorite);
        }

        [HttpGet("show/byCLient")]
        public async Task<object> GetFavoritesByClientId(Guid id)
        {
            var favorite = await _favoriteRepository.GetFavoritesByClientId(id);
            return new ApiResponse(HttpStatusCode.OK, favorite);
        }

        [HttpGet("show/byRecipe")]
        public async Task<object> GetFavoritesByRecipeId(Guid id)
        {
            var favorite = await _favoriteRepository.GetFavoritesByRecipeId(id);
            return new ApiResponse(HttpStatusCode.OK, favorite);
        }
    }
}
