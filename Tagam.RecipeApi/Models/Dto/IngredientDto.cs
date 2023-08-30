using System.ComponentModel.DataAnnotations;

namespace Tagam.RecipeApi.Models.Dto
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}