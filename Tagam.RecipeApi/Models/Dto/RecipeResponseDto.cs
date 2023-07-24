using Tagam.RecipeApi.Enums;

namespace Tagam.RecipeApi.Models.Dto
{
    public class RecipeResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int ImageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public TimeForPreparing TimeForPreparing { get; set; }
    }
}
