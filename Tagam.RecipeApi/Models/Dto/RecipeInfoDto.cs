using Tagam.RecipeApi.Enums;

namespace Tagam.RecipeApi.Models.Dto
{
    public class RecipeInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public Guid ClientId { get; set; }
        public virtual CategoryDto? Category { get; set; }
        public virtual TypeKitchenDto? TypeKitchen { get; set; }
        //public double Rating { get; set; }
        public int ImageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public TimeForPreparing TimeForPreparing { get; set; }
        public virtual ICollection<RecipeStepsDto> Steps { get; set; }
        public virtual ICollection<IngredientRecipeResponse> IngredientRecipes { get; set; }
        public RecipeInfoDto()
        {
            Steps = new List<RecipeStepsDto>();
            IngredientRecipes = new List<IngredientRecipeResponse>();
        }
    }
}
