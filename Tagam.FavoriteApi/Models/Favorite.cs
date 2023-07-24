using System.ComponentModel.DataAnnotations;

namespace Tagam.FavoriteApi.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid RecipeId { get; set; }
        [Required]
        public Guid ClientId { get; set; }
    }
}
