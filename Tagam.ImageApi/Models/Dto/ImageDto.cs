namespace Tagam.ImageApi.Models.Dto
{
    public class ImageDto
    {
        public Guid? RecipeId { get; set; } = null;
        public Guid? RecipeStepsId { get; set; } = null;
        public byte[] ImageData { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
