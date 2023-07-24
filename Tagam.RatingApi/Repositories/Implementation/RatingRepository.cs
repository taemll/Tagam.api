using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Tagam.RatingApi.Database;
using Tagam.RatingApi.Models;
using Tagam.RatingApi.Models.Dto;

namespace Tagam.RatingApi.Repositories.Implementation
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<IRatingRepository> _logger;
        private double averageRating = 0.0;

        public RatingRepository(DataContext context, IMapper mapper, ILogger<IRatingRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> AddRatingToRecipe(RatingDto rating)
        {
            _logger.LogInformation(nameof(AddRatingToRecipe));
            var domainRating = _mapper.Map<Rating>(rating);
            try
            {
                await _context.Ratings.AddAsync(domainRating);
                await _context.SaveChangesAsync();

                return "success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.Message;
            }
        }

        public async Task<double> CalculateAverageRating(Guid recipeId)
        {
            _logger.LogInformation(nameof(CalculateAverageRating));

            var ratings = await _context.Ratings.Where(r => r.RecipeId == recipeId).ToListAsync();

            if (ratings.Count > 0)
            {
                averageRating = ratings.Average(r => r.Remark);
            }

            return Math.Round(averageRating, 2);
        }
    }
}
