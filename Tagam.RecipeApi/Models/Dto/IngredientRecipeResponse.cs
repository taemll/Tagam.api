using Tagam.RecipeApi.Enums;

namespace Tagam.RecipeApi.Models.Dto
{
    public class IngredientRecipeResponse
    {
        public int Count { get; set; }
        public Dimension Dimension { get; set; }
        public virtual IngredientDto Ingredient { get; set; }
    }
}
