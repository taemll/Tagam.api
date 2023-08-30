using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tagam.RecipeApi.Enums;

namespace Tagam.RecipeApi.Models
{
    public class Recipe
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; } = null;
        public string Description { get; set; }
        //public Guid ClientId { get; set; }
        public int? TypeKitchenId { get; set; } = null;
        public int? CategoryId { get; set; } = null;
        public int? ImageId { get; set; } = null;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public TimeForPreparing TimeForPreparing { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
        [ForeignKey(nameof(TypeKitchenId))]
        public virtual TypeKitchen? TypeKitchen { get; set; }
        public virtual ICollection<RecipeSteps> Steps { get; set; }
        public virtual ICollection<IngredientRecipe> IngredientRecipes { get; set; }
        public Recipe()
        {
            Steps = new List<RecipeSteps>();
            IngredientRecipes = new List<IngredientRecipe>();
        }
    }
}
