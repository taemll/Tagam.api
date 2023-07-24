using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tagam.RecipeApi.ApiResponses;
using Tagam.RecipeApi.Models.Dto;
using Tagam.RecipeApi.Repositories;

namespace Tagam.RecipeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeKitchenController : Controller
    {
        private readonly ITypeKitchenRepository _typeKitchenRepository;

        public TypeKitchenController(ITypeKitchenRepository typeKitchenRepository)
        {
            _typeKitchenRepository = typeKitchenRepository ?? throw new ArgumentNullException(nameof(typeKitchenRepository));
        }

        [HttpGet("show")]
        public async Task<object> GetTypesKitchen()
        {
            try
            {
                var typesKitchen = await _typeKitchenRepository.GetTypesKitchen();
                return new ApiResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
        }

        [HttpGet("show-by-KitchenId")]
        public async Task<object> GetTypeKitchenById(int id)
        {
            try
            {
                var typeKitchen = await _typeKitchenRepository.GetTypeKitchenById(id);
                return new ApiResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<object> CreateTypeKitchen(TypeKitchenDto typeKitchen)
        {
            try
            {
                var newypeKitchen = await _typeKitchenRepository.CreateTypeKitchen(typeKitchen);
                return new ApiResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
