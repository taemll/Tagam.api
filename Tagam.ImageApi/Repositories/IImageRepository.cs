using Microsoft.AspNetCore.Mvc;
using Tagam.ImageApi.Models.Dto;

namespace Tagam.ImageApi.Repositories
{
    public interface IImageRepository
    {
        Task<ImageDto> CreateImage(Guid recipeId, byte[] imageData);
        Task<FileResult> GetImageByRecipeId(Guid id);
        Task<FileResult> GetImageById(int id);
        Task<FileResult> GetImageByStepId(Guid id);
        Task<string> DeleteImage(int id);
    }
}
