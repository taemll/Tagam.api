using System.ComponentModel.DataAnnotations;
using Tagam.RecipeApi.Enums;

namespace Tagam.RecipeApi.Models.Dto
{
    public class IngredientRecipeDto
    {
        public Guid RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Count { get; set; }
        public Dimension Dimension { get; set; }
    }
}
