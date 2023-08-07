using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Models
{
    public class RecipeBoxContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public RecipeBoxContext(DbContextOptions options) : base(options) { }
    }
}