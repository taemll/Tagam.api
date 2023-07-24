using System.ComponentModel.DataAnnotations;

namespace Tagam.RecipeApi.Models.Dto
{
    public class RecipeStepsDto
    {
        public string Description { get; set; }
        public Guid RecipeId { get; set; }
        public int ImageId { get; set; }
    }
}
