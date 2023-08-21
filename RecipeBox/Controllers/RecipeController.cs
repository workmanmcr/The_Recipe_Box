using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;


namespace RecipeBox.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly RecipeBoxContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<ActionResult> Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            List<Recipe> userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).Include(recipe => recipe.JoinEntities).ToList();
            return View(userRecipes);
        }
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Recipe recipe, int IngredientId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
                return View(recipe);
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
                recipe.User = currentUser;
                _db.Recipes.Add(recipe);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            Recipe thisRecipe = _db.Recipes.Include(recipe => recipe.JoinEntities).ThenInclude(join => join.Ingredient).Include(recipe => recipe.JoinTags).ThenInclude(join => join.Tag).FirstOrDefault(recipe => recipe.RecipeId == id);
            return View(thisRecipe);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
            ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
            return View(thisRecipe);
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipe)
        {
            _db.Recipes.Update(recipe);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
            return View(thisRecipe);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe thisRecipe = _db.Recipes
                                 .FirstOrDefault(recipe => recipe.RecipeId == id);
            _db.Recipes.Remove(thisRecipe);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult AddIngredient(int id)
        {
            Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
            ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
            return View(thisRecipe);
        }

        [HttpPost]
        public ActionResult AddIngredient(Recipe recipe, int ingredientId)
        {
#nullable enable
            RecipeIngredient? joinEntity = _db.RecipeIngredients.FirstOrDefault(join => (join.RecipeId == recipe.RecipeId && join.IngredientId == ingredientId));
#nullable disable

            if (joinEntity == null && recipe.RecipeId != 0)
            {
                _db.RecipeIngredients.Add(new RecipeIngredient() { RecipeId = recipe.RecipeId, IngredientId = ingredientId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = recipe.RecipeId });
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteIngredient(int joinId)
        {
            RecipeIngredient joinEntry = _db.RecipeIngredients.FirstOrDefault(entry => entry.RecipeIngredientId == joinId);
            _db.RecipeIngredients.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult AddTag(int id)
        {
            Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
            ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Title");
            return View(thisRecipe);
        }

        [HttpPost]
        public ActionResult AddTag(Recipe recipe, int tagId)
        {
#nullable enable
            RecipeTag? joinEntity = _db.RecipeTags.FirstOrDefault(join => (join.RecipeId == recipe.RecipeId && join.TagId == tagId));
#nullable disable

            if (joinEntity == null && recipe.RecipeId != 0)
            {
                _db.RecipeTags.Add(new RecipeTag() { RecipeId = recipe.RecipeId, TagId = tagId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = recipe.RecipeId });
        }

        public ActionResult AddUser(int id)
        {
            Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
            ViewBag.Id = new SelectList(_db.AspNetUsers, "Id", "UserName");
            return View(thisRecipe);
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(Recipe recipe, string id)
        {
#nullable enable
            ApplicationUser currentUser = await _userManager.FindByIdAsync(id);
            RecipeUser? joinEntity = _db.RecipeUser.FirstOrDefault(join => (join.RecipeId == recipe.RecipeId && join.User.Id == id));
#nullable disable

            if (joinEntity == null && recipe.RecipeId != 0)
            {
                _db.RecipeUser.Add(new RecipeUser() { RecipeId = recipe.RecipeId, User = currentUser });
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SharedRecipes()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<RecipeUser> sharedRecipes = _db.RecipeUser.Where(entry => entry.User.Id == userId).Include(recipe => recipe.Recipe).ToList();
            return View(sharedRecipes);
        }
    }
}


