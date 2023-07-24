using Microsoft.EntityFrameworkCore;
using Tagam.RecipeApi.Models;

namespace Tagam.RecipeApi.Database
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeSteps> RecipeSteps { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<TypeKitchen> TypesKitchen { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<IngredientRecipe> IngredientRecipes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Yorozuya;Initial Catalog=recipes2.0;Trusted_Connection=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Category>()
                .HasMany(e => e.Recipes)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired(false);
            
            builder.Entity<TypeKitchen>()
                .HasMany(e => e.Recipes)
                .WithOne(e => e.TypeKitchen)
                .HasForeignKey(e => e.TypeKitchenId)
                .IsRequired(false);

            builder.Entity<IngredientRecipe>()
                .HasOne(e => e.Recipe)
                .WithMany(e => e.IngredientRecipes)
                .HasForeignKey(e => e.RecipeId)
                .IsRequired(false);
            
            builder.Entity<IngredientRecipe>()
                .HasOne(e => e.Ingredient)
                .WithMany(e => e.IngredientRecipes)
                .HasForeignKey(e => e.IngredientId)
                .IsRequired(false);

            base.OnModelCreating(builder);

        }
    }
}
