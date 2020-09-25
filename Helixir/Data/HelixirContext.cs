using Helixir.Models;
using Microsoft.EntityFrameworkCore;

namespace Helixir.Data
{
    public class HelixirContext : DbContext
    {
        public HelixirContext(DbContextOptions<HelixirContext> options) : base(options)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Volume> Volumes { get; set; }
        
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>().ToTable("Drinks");
            
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            
            modelBuilder.Entity<Volume>().ToTable("Volumes").HasKey(v => new { v.IngredientId, v.DrinkId });
            modelBuilder.Entity<Volume>()
                .HasOne<Drink>()
                .WithMany(e => e.Volumes)
                .HasForeignKey(k => k.DrinkId);
            modelBuilder.Entity<Volume>()
                .HasOne<Ingredient>()
                .WithMany(e => e.Volumes)
                .HasForeignKey(k => k.IngredientId);

            modelBuilder.Entity<Score>()
                .ToTable("Scores")
                .HasOne<Drink>()
                .WithOne()
                .HasForeignKey<Score>(f => f.DrinkId);
        }
    }
}