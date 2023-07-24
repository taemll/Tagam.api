using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tagam.ImageApi.Database;
using Tagam.ImageApi.Models.Dto;
using Tagam.ImageApi.Models;

namespace Tagam.ImageApi.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<IImageRepository> _logger;

        public ImageRepository(DataContext context, IMapper mapper, ILogger<IImageRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<FileResult> GetImageById(int id)
        {
            _logger.LogInformation(nameof(GetImageById));

            var image = await _context.Images.Where(w => w.Id == id).FirstOrDefaultAsync();
            byte[] imageData = image.ImageData;

            if (image is null)
            {
                _logger.LogInformation("image is null");

                throw new ArgumentNullException($"По данному ID = {id} отстуствуют фото");
            }

            return new FileContentResult(imageData, "image/jpeg");
        }

        public async Task<FileResult> GetImageByRecipeId(Guid id)
        {
            _logger.LogInformation(nameof(GetImageByRecipeId));

            var image = await _context.Images.Where(w =>w.RecipeId == id).FirstOrDefaultAsync();
            byte[] imageData = image.ImageData;

            if (image is null)
            {
                _logger.LogInformation("image is null");

                throw new ArgumentNullException($"По данному ID = {id} отстуствуют фото");
            }

            return new FileContentResult(imageData, "image/jpeg");
        }

        public async Task<FileResult> GetImageByStepId(Guid id)
        {
            _logger.LogInformation(nameof(GetImageByStepId));

            var image = await _context.Images.Where(w => w.RecipeStepsId == id).FirstOrDefaultAsync();
            byte[] imageData = image.ImageData;

            if (image is null)
            {
                _logger.LogInformation("image is null");

                throw new ArgumentNullException($"По данному ID = {id} отстуствуют фото");
            }

            return new FileContentResult(imageData, "image/jpeg");
        }

        public async Task<ImageDto> CreateImage(Guid RecipeId, byte[] ImageData)
        {
            _logger.LogInformation(nameof(CreateImage));

            /*var image = new Image
            {
                RecipeId = recipeId,
                RecipeStepsId = recipeStepsId,
                ImageData = imageData
            };*/

            var domainImage = _mapper.Map<Image>(RecipeId);
            domainImage = _mapper.Map<Image>(ImageData);
            var createdImage = await _context.Images.AddAsync(domainImage);
            await _context.SaveChangesAsync();

            return _mapper.Map<ImageDto>(createdImage.Entity);
        }

        public async Task<string> DeleteImage(int id)
        {
            _logger.LogInformation(nameof(DeleteImage));

            var image = await _context.Images.FindAsync(id);

            if (image is null)
            {
                _logger.LogInformation("image is null");
                throw new ArgumentNullException(nameof(image));
            }

            try
            {
                _context.Images.Remove(image);
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
