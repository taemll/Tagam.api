using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tagam.ImageApi.ApiResponses;
using Tagam.ImageApi.Repositories;

namespace Tagam.ImageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
        }

        [HttpPost("create")]
        public async Task<object> UploadImageRecipe(Guid recipeId, IFormFile imageFile)
        {
            if (imageFile is null || imageFile.Length == 0)
            {
                return BadRequest("image file not found.");
            }

            byte[] ImageData;
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                ImageData = memoryStream.ToArray();
            }

            var newImage = await _imageRepository.CreateImage(recipeId, ImageData);
            return new ApiResponse(System.Net.HttpStatusCode.OK, ImageData);
        }

        [HttpGet("showByRecipeId")]
        public async Task<object> GetImageByRecipeId(Guid recipeId)
        {
            try
            {
                var image = await _imageRepository.GetImageByRecipeId(recipeId);
                return new ApiResponse(System.Net.HttpStatusCode.OK, image);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet("showById")]
        public async Task<object> GetImageById(int id)
        {
            try
            {
                var image = await _imageRepository.GetImageById(id);
                return new ApiResponse(System.Net.HttpStatusCode.OK, image);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("showByStepsId")]
        public async Task<object> GetImageByStepId(Guid id)
        {
            try
            {
                var image = await _imageRepository.GetImageByStepId(id);
                return new ApiResponse(System.Net.HttpStatusCode.OK, image);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet("delete")]
        public async Task<object> DeleteImage(int id)
        {
            try
            {
                var image = await _imageRepository.DeleteImage(id);
                return new ApiResponse(System.Net.HttpStatusCode.OK, image);
            }
            catch (Exception ex)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
