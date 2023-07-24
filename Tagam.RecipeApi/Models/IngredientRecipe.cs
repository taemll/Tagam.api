using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tagam.RecipeApi.Enums;

namespace Tagam.RecipeApi.Models
{
    public class IngredientRecipe
    {
        [Key]
        public int Id { get; set; }
        public Guid RecipeId { get; set; }
        public int IngredientId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public Dimension Dimension { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public virtual Recipe Recipe { get; set; }
        [ForeignKey(nameof(IngredientId))]
        public virtual Ingredient Ingredient { get; set; }
    }
}
