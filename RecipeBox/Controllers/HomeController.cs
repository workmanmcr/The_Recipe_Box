using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using RecipeBox.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using RestSharp;

namespace RecipeBox.Controllers
{
public class HomeController : Controller
{
    private readonly RecipeBoxContext _db;
    private readonly string _apiKey;

    public HomeController(RecipeBoxContext db, IConfiguration configuration)
    {
        _db = db;
        _apiKey = configuration["MealDb"];
        }

    [HttpGet("/")]
    public ActionResult Index()
    {
        Recipe[] recipes = _db.Recipes.OrderByDescending(recipe => recipe.Rating).ToArray();
        Ingredient[] ingredients = _db.Ingredients.ToArray();
        Dictionary<string, object[]> model = new Dictionary<string, object[]>();
        model.Add("recipes", recipes);
        model.Add("ingredients", ingredients);
        return View(model);

    }
    public ActionResult Search(string searchTerm)
    {
        List<Recipe> recipeResults = _db.Recipes
            .Where(recipe => recipe.Name.Contains(searchTerm) || recipe.Instruction.Contains(searchTerm))
            .ToList();

        List<Ingredient> ingredientResults = _db.Ingredients
            .Where(ingredient => ingredient.IngredientName.Contains(searchTerm))
            .ToList();

        var searchResults = new SearchResults
        {
            RecipeResults = recipeResults,
            IngredientResults = ingredientResults
        };

        return View("SearchResults", searchResults);
    }

    public ActionResult MealOfTheDay()
    {
        Task<RandomMeal> meal = RandomMeal.GetMeal(_apiKey);
        return View(meal);
    }

}
}