using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Models
{
    public class RecipeBoxContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }
        public DbSet<RecipeUser> RecipeUser { get; set; }
        public RecipeBoxContext(DbContextOptions options) : base(options) { }
    }
}