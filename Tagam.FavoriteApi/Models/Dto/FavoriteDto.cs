using System.ComponentModel.DataAnnotations;

namespace Tagam.FavoriteApi.Models.Dto
{
    public class FavoriteDto
    {
        public Guid RecipeId { get; set; }
        public Guid ClientId { get; set; }
    }
}
