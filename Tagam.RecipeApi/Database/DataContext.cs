using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Tagam.RecipeApi.Models;

namespace Tagam.RecipeApi.Database
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options) {
            try
            {
                var databaseCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreater != null)
                {
                    if (!databaseCreater.CanConnect())
                    {
                        databaseCreater.Create();
                    }
                    if (!databaseCreater.HasTables())
                    {
                        databaseCreater.CreateTables();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeSteps> RecipeSteps { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<TypeKitchen> TypesKitchen { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<IngredientRecipe> IngredientRecipes { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=mssql;Initial Catalog=recipes;Password=Qwerty23#");
        //    base.OnConfiguring(optionsBuilder);
        //}
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
