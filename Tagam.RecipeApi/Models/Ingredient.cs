using System.ComponentModel.DataAnnotations;

namespace Tagam.RecipeApi.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<IngredientRecipe> IngredientRecipes { get; set; }
        public Ingredient()
        {
            IngredientRecipes = new List<IngredientRecipe>();
        }
    }
}
