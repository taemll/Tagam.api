using Microsoft.EntityFrameworkCore;
using Tagam.FavoriteApi.Models;

namespace Tagam.FavoriteApi.Database
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

        public DbSet<Favorite> Favorites { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Yorozuya;Initial Catalog=recipes2.0;Trusted_Connection=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

        }
    }
}
