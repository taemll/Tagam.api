using System.ComponentModel.DataAnnotations;

namespace Tagam.RecipeApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } = null!;
        public string ImgLink { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public Category()
        {
            Recipes= new List<Recipe>();
        }
    }
}
