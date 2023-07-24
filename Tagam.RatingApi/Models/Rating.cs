using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Tagam.RatingApi.Models
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(2)]
        public double Remark { get; set; }

        [StringLength(200)]
        public string? Comment { get; set; } = null;

        [Required]
        public Guid RecipeId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
