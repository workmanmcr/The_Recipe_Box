using RecipeBox.Models;
namespace RecipeBox.ViewModels
{
public class SearchResults
{
    public List<Recipe> RecipeResults { get; set; }
    public List<Ingredient> IngredientResults { get; set; }
}
}