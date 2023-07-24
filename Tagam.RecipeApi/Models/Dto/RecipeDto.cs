using Tagam.RecipeApi.Enums;

namespace Tagam.RecipeApi.Models.Dto
{
    public class RecipeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public Guid ClientId { get; set; }
        public int TypeKitchenId { get; set; }
        public int CategoryId { get; set; }
        //public double Rating { get; set; }
        public int ImageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public TimeForPreparing TimeForPreparing { get; set; }
    }
}