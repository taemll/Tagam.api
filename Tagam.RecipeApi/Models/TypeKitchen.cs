using System.ComponentModel.DataAnnotations;

namespace Tagam.RecipeApi.Models
{
    public class TypeKitchen
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 
        public virtual ICollection<Recipe> Recipes { get; set; }
        public TypeKitchen()
        {
            Recipes = new List<Recipe>();
        }
    }
}
