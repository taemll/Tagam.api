using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tagam.RecipeApi.ApiResponses;
using Tagam.RecipeApi.Models.Dto;
using Tagam.RecipeApi.Repositories;

namespace Tagam.RecipeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        [HttpGet]
        [Route("show")]
        public async Task<object> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            return new ApiResponse(HttpStatusCode.OK, categories);
        }

        [HttpPost]
        [Route("showById/{id}")]
        public async Task<object> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryById(id);
                return new ApiResponse(HttpStatusCode.OK, category);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPost]
        [Route("create")]
        public async Task<object> CreateCategory(CategoryDto category)
        {
            var newCategory = await _categoryRepository.CreateCategory(category);
            return new ApiResponse(HttpStatusCode.OK, newCategory);
        }
    }
}
