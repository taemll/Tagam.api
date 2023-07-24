using System.ComponentModel.DataAnnotations;

namespace Tagam.ImageApi.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public Guid? RecipeId { get; set; } = null;
        public Guid? RecipeStepsId { get; set; } = null;
        public byte[] ImageData { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
