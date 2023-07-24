using System.ComponentModel.DataAnnotations;

namespace Tagam.RatingApi.Models.Dto
{
    public class RatingDto
    {
        public double Remark { get; set; }
        public string? Comment { get; set; } = null;
        public Guid RecipeId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
