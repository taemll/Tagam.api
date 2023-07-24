using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tagam.RecipeApi.Models
{
    public class RecipeSteps
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid RecipeId { get; set; }
        public int ImageId { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public virtual Recipe? Recipe { get; set; }
    }
}
