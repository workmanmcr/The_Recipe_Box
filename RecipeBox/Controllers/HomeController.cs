using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using RecipeBox.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using RestSharp;
using Microsoft.Extensions.Configuration;


namespace RecipeBox.Controllers
{
public class HomeController : Controller
{
    private readonly RecipeBoxContext _db;
    private readonly string _apiKey;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager, RecipeBoxContext db, IConfiguration configuration)
    {
        _userManager = userManager;
        _db = db;
        _apiKey = configuration["MealDb"];
    }

    [HttpGet("/")]
    public async Task<ActionResult> Index()
    {
        Ingredient[] ingredients = _db.Ingredients.ToArray();
        Dictionary<string, object[]> model = new Dictionary<string, object[]>();
        string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        if (currentUser != null)
        {
            Recipe[] recipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).ToArray();
            model.Add("recipes", recipes);
            model.Add("ingredients", ingredients);
            return View(model);
        }
        else
        {
            model.Add("recipes", new Recipe[1]);
            model.Add("ingredients", new Ingredient[1]);
            return View(model);
        }
        
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

    public async Task<IActionResult> MealOfTheDay()
    {
        RandomMeal meal = await RandomMeal.GetMeal(_apiKey);
    return View("MealOfTheDay", meal);
    }

}
}